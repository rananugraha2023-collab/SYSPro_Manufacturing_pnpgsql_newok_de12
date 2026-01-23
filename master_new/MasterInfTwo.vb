Imports npgsql
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export

Public Class MasterInfTwo
    Public Overridable Sub set_window(ByVal arg As MasterMDI)
        fmm = arg
    End Sub

    Dim mf As New ModFunction
    Public _rpt As New DevExpress.XtraReports.UI.XtraReport
    Public _assembly As Reflection.Assembly = Me.GetType.Assembly

    Private Sub MasterInfTwo_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        load_when_active()
    End Sub

    Public Overridable Sub load_when_active()
        Dim my As MasterMDI = CType(Me.ParentForm, MasterMDI)
        my.configurasi_menu("informasi")
    End Sub

    Public Overrides Sub form_first_load()
        'create_table()
        load_data_many(False)
        load_cb()
        on_load()
        format_grid()
        add_groupsummary()
        AllowIncrementalSearch()
        load_Columns()
        '========= setting grid style
        'fmm.bsi_gridstyle.Caption = "Grid : " + master_new.ClsVar.sGridStyle
        style_grid_detail()
        '============================

        'seting embeddednavigator dan style grid
        '================================================================================================
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
                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                        For Each ctrl_xtp In xtp.Controls
                                            If TypeOf ctrl_xtp Is DevExpress.XtraGrid.GridControl Then
                                                gc = CType(ctrl_xtp, DevExpress.XtraGrid.GridControl)
                                                gc.UseEmbeddedNavigator = True
                                                format_embeddednavigator(gc)

                                                gv = gc.MainView
                                                gv.OptionsView.ColumnAutoWidth = False
                                                gv.OptionsView.ShowAutoFilterRow = True
                                                gv.OptionsView.ShowFooter = True
                                                gv.OptionsView.ShowGroupedColumns = True
                                                gv.OptionsSelection.MultiSelect = True

                                                style_grid(gv, master_new.ClsVar.sGridStyle)
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
        '================================================================================================
    End Sub

    Public Overrides Sub style_grid_detail()
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraBars.Docking.DockPanel Then
                dp = CType(ctrl, DevExpress.XtraBars.Docking.DockPanel)
                For Each ctrl_dp In dp.Controls
                    If TypeOf ctrl_dp Is DevExpress.XtraBars.Docking.ControlContainer Then
                        cc = CType(ctrl_dp, DevExpress.XtraBars.Docking.ControlContainer)
                        For Each ctrl_cc In cc.Controls
                            If TypeOf ctrl_cc Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_cc, DevExpress.XtraTab.XtraTabControl)
                                For Each ctrl_xtc In xtc.Controls
                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                        For Each ctrl_xtp In xtp.Controls
                                            If TypeOf ctrl_xtp Is DevExpress.XtraEditors.SplitContainerControl Then
                                                spcd2 = CType(ctrl_xtp, DevExpress.XtraEditors.SplitContainerControl)
                                                For Each ctrl_spcd2 In spcd2.Controls
                                                    If TypeOf ctrl_spcd2 Is DevExpress.XtraEditors.SplitGroupPanel Then
                                                        sgpd2 = CType(ctrl_spcd2, DevExpress.XtraEditors.SplitGroupPanel)
                                                        For Each ctrl_sgpd2 In sgpd2.Controls
                                                            If TypeOf ctrl_sgpd2 Is DevExpress.XtraGrid.GridControl Then
                                                                Dim gc_detail As New DevExpress.XtraGrid.GridControl
                                                                gc_detail = CType(ctrl_sgpd2, DevExpress.XtraGrid.GridControl)
                                                                gc_detail.UseEmbeddedNavigator = True
                                                                format_embeddednavigator(gc_detail)

                                                                Dim gv_detail As New DevExpress.XtraGrid.Views.Grid.GridView
                                                                gv_detail = gc_detail.MainView
                                                                gv_detail.OptionsView.ColumnAutoWidth = False
                                                                gv_detail.OptionsView.ShowAutoFilterRow = False
                                                                gv_detail.OptionsView.ShowFooter = False
                                                                gv_detail.OptionsView.ShowGroupPanel = False
                                                                gv_detail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
                                                                gv_detail.OptionsView.ShowGroupedColumns = True
                                                                gv_detail.OptionsSelection.MultiSelect = True

                                                                style_grid(gv_detail, master_new.ClsVar.sGridStyle)
                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            ElseIf TypeOf ctrl_xtp Is DevExpress.XtraTab.XtraTabControl Then
                                                xtcd = CType(ctrl_xtp, DevExpress.XtraTab.XtraTabControl)
                                                For Each ctrl_xtcd In xtcd.Controls
                                                    If TypeOf ctrl_xtcd Is DevExpress.XtraTab.XtraTabPage Then
                                                        xtpd = CType(ctrl_xtcd, DevExpress.XtraTab.XtraTabPage)
                                                        For Each ctrl_xtpd In xtpd.Controls
                                                            If TypeOf ctrl_xtpd Is DevExpress.XtraEditors.SplitContainerControl Then
                                                                spcd2 = CType(ctrl_xtpd, DevExpress.XtraEditors.SplitContainerControl)
                                                                For Each ctrl_spcd2 In spcd2.Controls
                                                                    If TypeOf ctrl_spcd2 Is DevExpress.XtraEditors.SplitGroupPanel Then
                                                                        sgpd2 = CType(ctrl_spcd2, DevExpress.XtraEditors.SplitGroupPanel)
                                                                        For Each ctrl_sgpd2 In sgpd2.Controls
                                                                            If TypeOf ctrl_sgpd2 Is DevExpress.XtraGrid.GridControl Then
                                                                                Dim gc_detail As New DevExpress.XtraGrid.GridControl
                                                                                gc_detail = CType(ctrl_sgpd2, DevExpress.XtraGrid.GridControl)
                                                                                gc_detail.UseEmbeddedNavigator = True
                                                                                format_embeddednavigator(gc_detail)

                                                                                Dim gv_detail As New DevExpress.XtraGrid.Views.Grid.GridView
                                                                                gv_detail = gc_detail.MainView
                                                                                gv_detail.OptionsView.ColumnAutoWidth = False
                                                                                gv_detail.OptionsView.ShowAutoFilterRow = False
                                                                                gv_detail.OptionsView.ShowFooter = False
                                                                                gv_detail.OptionsView.ShowGroupPanel = False
                                                                                gv_detail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Default
                                                                                gv_detail.OptionsView.ShowGroupedColumns = True
                                                                                gv_detail.OptionsSelection.MultiSelect = True

                                                                                style_grid(gv_detail, master_new.ClsVar.sGridStyle)
                                                                            End If
                                                                        Next
                                                                    End If
                                                                Next
                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Public Overrides Sub add_groupsummary()
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
                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                        For Each ctrl_xtp In xtp.Controls
                                            If TypeOf ctrl_xtp Is DevExpress.XtraGrid.GridControl Then
                                                gc = CType(ctrl_xtp, DevExpress.XtraGrid.GridControl)
                                                gv = gc.MainView
                                                gv.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "groupsummary")
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Public Overrides Sub AllowIncrementalSearch()
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
                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                        For Each ctrl_xtp In xtp.Controls
                                            If TypeOf ctrl_xtp Is DevExpress.XtraGrid.GridControl Then
                                                gc = CType(ctrl_xtp, DevExpress.XtraGrid.GridControl)
                                                gv = gc.MainView
                                                gv.OptionsBehavior.AllowIncrementalSearch = True
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Public Overrides Sub bestfit_column()
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
                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                        For Each ctrl_xtp In xtp.Controls
                                            If TypeOf ctrl_xtp Is DevExpress.XtraGrid.GridControl Then
                                                gc = CType(ctrl_xtp, DevExpress.XtraGrid.GridControl)
                                                gv = gc.MainView
                                                gv.BestFitColumns()
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Public Overrides Sub find()
        load_data_many(True)
    End Sub

    Public Overrides Sub refresh_data()
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            'If ctrl_sgp.name.Substring(0, 6) = "pr_txt" Then
                            '    If TypeOf ctrl_sgp Is TextBox Then
                            '        ctrl_sgp.Text = "%"
                            '    ElseIf TypeOf ctrl_sgp Is DateTimePicker Then
                            '        If ctrl_sgp.Name.ToLower = "pr_txttglawal" Then
                            '            ctrl_sgp.Value = CDate(Month(Today.Date).ToString + "/" + Year(Today.Date).ToString)
                            '        ElseIf ctrl_sgp.Name.ToLower = "pr_txttglakhir" Then
                            '            ctrl_sgp.Value = Today.Date()
                            '        End If
                            '    ElseIf TypeOf ctrl_sgp Is ComboBox Then
                            '        ctrl_sgp.text = ""
                            '    ElseIf TypeOf ctrl_sgp Is NumericUpDown Then
                            '        ctrl_sgp.Value = ctrl_sgp.Minimum
                            '    End If
                            'End If
                        Next
                    End If
                Next
            End If
        Next

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
                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                        For Each ctrl_xtp In xtp.Controls
                                            If TypeOf ctrl_xtp Is DevExpress.XtraGrid.GridControl Then
                                                gc = CType(ctrl_xtp, DevExpress.XtraGrid.GridControl)
                                                gv = gc.MainView
                                                gv.ActiveFilter.Clear()
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
        help_load_data(True)
    End Sub

    Public Overridable Sub retrieve_data()

    End Sub



    'Protected Overrides Sub Export_Progress(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Export.ProgressEventArgs)
    '    If e.Phase = DevExpress.XtraGrid.Export.ExportPhase.Link Then

    '        progressForm.SetProgressValue(e.Position)
    '    End If
    'End Sub

#Region "style_grid"
    Public Overrides Sub style_grid(ByVal gv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal par_style As String)
        If IsDBNull(gv) = False Then
            If par_style = "default" Then
                gv.Appearance.Reset()
            ElseIf par_style = "Pastel#1" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(201, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(201, Byte), Integer))
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(234, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(234, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(201, Byte), Integer))
                gv.Appearance.Empty.BackColor2 = System.Drawing.Color.White
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(234, Byte), Integer))
                gv.Appearance.EvenRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(234, Byte), Integer))
                gv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.EvenRow.Options.UseBackColor = True
                gv.Appearance.EvenRow.Options.UseBorderColor = True
                gv.Appearance.EvenRow.Options.UseForeColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(179, Byte), Integer))
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(179, Byte), Integer))
                gv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterCloseButton.Options.UseForeColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(201, Byte), Integer))
                gv.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(122, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(113, Byte), Integer))
                gv.Appearance.FixedLine.Options.UseBackColor = True
                gv.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
                gv.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FocusedCell.Options.UseBackColor = True
                gv.Appearance.FocusedCell.Options.UseForeColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(157, Byte), Integer))
                gv.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(188, Byte), Integer))
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseBorderColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(170, Byte), Integer))
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(170, Byte), Integer))
                gv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.FooterPanel.Options.UseForeColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(179, Byte), Integer))
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(179, Byte), Integer))
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(201, Byte), Integer))
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(201, Byte), Integer))
                gv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.Options.UseForeColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(213, Byte), Integer))
                gv.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(201, Byte), Integer))
                gv.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(201, Byte), Integer))
                gv.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseBorderColor = True
                gv.Appearance.GroupRow.Options.UseForeColor = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(170, Byte), Integer))
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(170, Byte), Integer))
                gv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HeaderPanel.Options.UseForeColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(203, Byte), Integer))
                gv.Appearance.HideSelectionRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(203, Byte), Integer))
                gv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HideSelectionRow.Options.UseBorderColor = True
                gv.Appearance.HideSelectionRow.Options.UseForeColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(170, Byte), Integer))
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
                gv.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
                gv.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.OddRow.Options.UseBackColor = True
                gv.Appearance.OddRow.Options.UseBorderColor = True
                gv.Appearance.OddRow.Options.UseForeColor = True
                gv.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(247, Byte), Integer))
                gv.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
                gv.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(148, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(148, Byte), Integer))
                gv.Appearance.Preview.Options.UseBackColor = True
                gv.Appearance.Preview.Options.UseFont = True
                gv.Appearance.Preview.Options.UseForeColor = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
                gv.Appearance.Row.ForeColor = System.Drawing.Color.Black
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.Row.Options.UseForeColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(201, Byte), Integer))
                gv.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(188, Byte), Integer))
                gv.Appearance.SelectedRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(203, Byte), Integer))
                gv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.SelectedRow.Options.UseBackColor = True
                gv.Appearance.SelectedRow.Options.UseBorderColor = True
                gv.Appearance.SelectedRow.Options.UseForeColor = True
                gv.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
                gv.Appearance.TopNewRow.Options.UseBackColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(170, Byte), Integer))
                gv.Appearance.VertLine.Options.UseBackColor = True
            ElseIf par_style = "Pastel#2" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
                gv.Appearance.Empty.BackColor2 = System.Drawing.Color.White
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(245, Byte), Integer))
                gv.Appearance.EvenRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(245, Byte), Integer))
                gv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.EvenRow.Options.UseBackColor = True
                gv.Appearance.EvenRow.Options.UseBorderColor = True
                gv.Appearance.EvenRow.Options.UseForeColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
                gv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterCloseButton.Options.UseForeColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
                gv.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(122, Byte), Integer))
                gv.Appearance.FixedLine.Options.UseBackColor = True
                gv.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
                gv.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FocusedCell.Options.UseBackColor = True
                gv.Appearance.FocusedCell.Options.UseForeColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(177, Byte), Integer))
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(208, Byte), Integer))
                gv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.FooterPanel.Options.UseForeColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(188, Byte), Integer))
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(188, Byte), Integer))
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
                gv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.Options.UseForeColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
                gv.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
                gv.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(221, Byte), Integer))
                gv.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseBorderColor = True
                gv.Appearance.GroupRow.Options.UseForeColor = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(216, Byte), Integer))
                gv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HeaderPanel.Options.UseForeColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(186, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(215, Byte), Integer))
                gv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(134, Byte), Integer))
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HideSelectionRow.Options.UseForeColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(180, Byte), Integer))
                gv.Appearance.HorzLine.BorderColor = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(122, Byte), Integer))
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.HorzLine.Options.UseBorderColor = True
                gv.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
                gv.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
                gv.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.OddRow.Options.UseBackColor = True
                gv.Appearance.OddRow.Options.UseBorderColor = True
                gv.Appearance.OddRow.Options.UseForeColor = True
                gv.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(240, Byte), Integer))
                gv.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
                gv.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(134, Byte), Integer))
                gv.Appearance.Preview.Options.UseBackColor = True
                gv.Appearance.Preview.Options.UseFont = True
                gv.Appearance.Preview.Options.UseForeColor = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
                gv.Appearance.Row.ForeColor = System.Drawing.Color.Black
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.Row.Options.UseForeColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(236, Byte), Integer))
                gv.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(201, Byte), Integer), CType(CType(207, Byte), Integer))
                gv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.SelectedRow.Options.UseBackColor = True
                gv.Appearance.SelectedRow.Options.UseForeColor = True
                gv.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
                gv.Appearance.TopNewRow.Options.UseBackColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(180, Byte), Integer))
                gv.Appearance.VertLine.BorderColor = System.Drawing.Color.FromArgb(CType(CType(117, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(122, Byte), Integer))
                gv.Appearance.VertLine.Options.UseBackColor = True
                gv.Appearance.VertLine.Options.UseBorderColor = True
            ElseIf par_style = "Pastel#3" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(184, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(184, Byte), Integer))
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(211, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(211, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(240, Byte), Integer))
                gv.Appearance.Empty.BackColor2 = System.Drawing.Color.White
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(220, Byte), Integer))
                gv.Appearance.EvenRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(220, Byte), Integer))
                gv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.EvenRow.Options.UseBackColor = True
                gv.Appearance.EvenRow.Options.UseBorderColor = True
                gv.Appearance.EvenRow.Options.UseForeColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(184, Byte), Integer))
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(184, Byte), Integer))
                gv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterCloseButton.Options.UseForeColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(240, Byte), Integer))
                gv.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
                gv.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FocusedCell.Options.UseBackColor = True
                gv.Appearance.FocusedCell.Options.UseForeColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(184, Byte), Integer))
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(184, Byte), Integer))
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(184, Byte), Integer))
                gv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.FooterPanel.Options.UseForeColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(184, Byte), Integer))
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(184, Byte), Integer))
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(211, Byte), Integer))
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(211, Byte), Integer))
                gv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.Options.UseForeColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(240, Byte), Integer))
                gv.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(211, Byte), Integer))
                gv.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(211, Byte), Integer))
                gv.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseBorderColor = True
                gv.Appearance.GroupRow.Options.UseForeColor = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(211, Byte), Integer))
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(211, Byte), Integer))
                gv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HeaderPanel.Options.UseForeColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(220, Byte), Integer))
                gv.Appearance.HideSelectionRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(191, Byte), Integer))
                gv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(111, Byte), Integer))
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HideSelectionRow.Options.UseBorderColor = True
                gv.Appearance.HideSelectionRow.Options.UseForeColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(184, Byte), Integer))
                gv.Appearance.HorzLine.BorderColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(111, Byte), Integer))
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.HorzLine.Options.UseBorderColor = True
                gv.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(240, Byte), Integer))
                gv.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(240, Byte), Integer))
                gv.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.OddRow.Options.UseBackColor = True
                gv.Appearance.OddRow.Options.UseBorderColor = True
                gv.Appearance.OddRow.Options.UseForeColor = True
                gv.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(253, Byte), Integer), CType(CType(247, Byte), Integer))
                gv.Appearance.Preview.BorderColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(253, Byte), Integer), CType(CType(247, Byte), Integer))
                gv.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
                gv.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(111, Byte), Integer))
                gv.Appearance.Preview.Options.UseBackColor = True
                gv.Appearance.Preview.Options.UseBorderColor = True
                gv.Appearance.Preview.Options.UseFont = True
                gv.Appearance.Preview.Options.UseForeColor = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(240, Byte), Integer))
                gv.Appearance.Row.BorderColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(240, Byte), Integer))
                gv.Appearance.Row.ForeColor = System.Drawing.Color.Black
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.Row.Options.UseBorderColor = True
                gv.Appearance.Row.Options.UseForeColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(240, Byte), Integer))
                gv.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(205, Byte), Integer))
                gv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.SelectedRow.Options.UseBackColor = True
                gv.Appearance.SelectedRow.Options.UseForeColor = True
                gv.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
                gv.Appearance.TopNewRow.Options.UseBackColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(184, Byte), Integer))
                gv.Appearance.VertLine.Options.UseBackColor = True
            ElseIf par_style = "Winter" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.White
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
                gv.Appearance.Empty.BackColor2 = System.Drawing.Color.White
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
                gv.Appearance.EvenRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
                gv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.EvenRow.Options.UseBackColor = True
                gv.Appearance.EvenRow.Options.UseBorderColor = True
                gv.Appearance.EvenRow.Options.UseForeColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
                gv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.White
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterCloseButton.Options.UseForeColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
                gv.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(195, Byte), Integer))
                gv.Appearance.FixedLine.Options.UseBackColor = True
                gv.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
                gv.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FocusedCell.Options.UseBackColor = True
                gv.Appearance.FocusedCell.Options.UseForeColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(189, Byte), Integer))
                gv.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(206, Byte), Integer))
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseBorderColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
                gv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.FooterPanel.Options.UseForeColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
                gv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.Options.UseForeColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
                gv.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
                gv.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(254, Byte), Integer))
                gv.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseBorderColor = True
                gv.Appearance.GroupRow.Options.UseForeColor = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(201, Byte), Integer), CType(CType(254, Byte), Integer))
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(201, Byte), Integer), CType(CType(254, Byte), Integer))
                gv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HeaderPanel.Options.UseForeColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(225, Byte), Integer))
                gv.Appearance.HideSelectionRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(83, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(215, Byte), Integer))
                gv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HideSelectionRow.Options.UseBorderColor = True
                gv.Appearance.HideSelectionRow.Options.UseForeColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
                gv.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
                gv.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.OddRow.Options.UseBackColor = True
                gv.Appearance.OddRow.Options.UseBorderColor = True
                gv.Appearance.OddRow.Options.UseForeColor = True
                gv.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
                gv.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(83, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(215, Byte), Integer))
                gv.Appearance.Preview.Options.UseFont = True
                gv.Appearance.Preview.Options.UseForeColor = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
                gv.Appearance.Row.ForeColor = System.Drawing.Color.Black
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.Row.Options.UseForeColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
                gv.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(83, Byte), Integer), CType(CType(155, Byte), Integer), CType(CType(215, Byte), Integer))
                gv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.SelectedRow.Options.UseBackColor = True
                gv.Appearance.SelectedRow.Options.UseForeColor = True
                gv.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
                gv.Appearance.TopNewRow.Options.UseBackColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(184, Byte), Integer), CType(CType(251, Byte), Integer))
                gv.Appearance.VertLine.Options.UseBackColor = True
            ElseIf par_style = "Spring" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(162, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(102, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(162, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(102, Byte), Integer))
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(163, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(163, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(128, Byte), Integer))
                gv.Appearance.Empty.BackColor2 = System.Drawing.Color.White
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(173, Byte), Integer))
                gv.Appearance.EvenRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(173, Byte), Integer))
                gv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.EvenRow.Options.UseBackColor = True
                gv.Appearance.EvenRow.Options.UseBorderColor = True
                gv.Appearance.EvenRow.Options.UseForeColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(162, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(102, Byte), Integer))
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(162, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(102, Byte), Integer))
                gv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterCloseButton.Options.UseForeColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(128, Byte), Integer))
                gv.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(159, Byte), Integer), CType(CType(69, Byte), Integer))
                gv.Appearance.FixedLine.Options.UseBackColor = True
                gv.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
                gv.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FocusedCell.Options.UseBackColor = True
                gv.Appearance.FocusedCell.Options.UseForeColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(49, Byte), Integer))
                gv.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(62, Byte), Integer))
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseBorderColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(162, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(102, Byte), Integer))
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(162, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(102, Byte), Integer))
                gv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.FooterPanel.Options.UseForeColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(162, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(102, Byte), Integer))
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(162, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(102, Byte), Integer))
                gv.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupButton.Options.UseForeColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(128, Byte), Integer))
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(128, Byte), Integer))
                gv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.Options.UseForeColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(128, Byte), Integer))
                gv.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(128, Byte), Integer))
                gv.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(128, Byte), Integer))
                gv.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseBorderColor = True
                gv.Appearance.GroupRow.Options.UseForeColor = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(115, Byte), Integer))
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(115, Byte), Integer))
                gv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HeaderPanel.Options.UseForeColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(176, Byte), Integer), CType(CType(84, Byte), Integer))
                gv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HideSelectionRow.Options.UseForeColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(162, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(102, Byte), Integer))
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(163, Byte), Integer))
                gv.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(163, Byte), Integer))
                gv.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.OddRow.Options.UseBackColor = True
                gv.Appearance.OddRow.Options.UseBorderColor = True
                gv.Appearance.OddRow.Options.UseForeColor = True
                gv.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(220, Byte), Integer))
                gv.Appearance.Preview.BorderColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(220, Byte), Integer))
                gv.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
                gv.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(90, Byte), Integer))
                gv.Appearance.Preview.Options.UseBackColor = True
                gv.Appearance.Preview.Options.UseBorderColor = True
                gv.Appearance.Preview.Options.UseFont = True
                gv.Appearance.Preview.Options.UseForeColor = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(173, Byte), Integer))
                gv.Appearance.Row.ForeColor = System.Drawing.Color.Black
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.Row.Options.UseForeColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(128, Byte), Integer))
                gv.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(62, Byte), Integer))
                gv.Appearance.SelectedRow.Options.UseBackColor = True
                gv.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
                gv.Appearance.TopNewRow.Options.UseBackColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(162, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(102, Byte), Integer))
                gv.Appearance.VertLine.Options.UseBackColor = True
            ElseIf par_style = "Summer" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(48, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(48, Byte), Integer))
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.White
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(98, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(57, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(98, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(57, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.White
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(177, Byte), Integer))
                gv.Appearance.Empty.BackColor2 = System.Drawing.Color.White
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(177, Byte), Integer))
                gv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.EvenRow.Options.UseBackColor = True
                gv.Appearance.EvenRow.Options.UseForeColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(48, Byte), Integer))
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(48, Byte), Integer))
                gv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.White
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterCloseButton.Options.UseForeColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(177, Byte), Integer))
                gv.Appearance.FilterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(177, Byte), Integer))
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseBorderColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(98, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(37, Byte), Integer))
                gv.Appearance.FixedLine.Options.UseBackColor = True
                gv.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
                gv.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FocusedCell.Options.UseBackColor = True
                gv.Appearance.FocusedCell.Options.UseForeColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(50, Byte), Integer))
                gv.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(50, Byte), Integer))
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseBorderColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(48, Byte), Integer))
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(48, Byte), Integer))
                gv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.FooterPanel.Options.UseForeColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(48, Byte), Integer))
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(48, Byte), Integer))
                gv.Appearance.GroupButton.ForeColor = System.Drawing.Color.White
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupButton.Options.UseForeColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(55, Byte), Integer))
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(55, Byte), Integer))
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(177, Byte), Integer))
                gv.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(55, Byte), Integer))
                gv.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(55, Byte), Integer))
                gv.Appearance.GroupRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseBorderColor = True
                gv.Appearance.GroupRow.Options.UseForeColor = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(98, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(57, Byte), Integer))
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(98, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(57, Byte), Integer))
                gv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HeaderPanel.Options.UseForeColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(93, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(64, Byte), Integer))
                gv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(177, Byte), Integer))
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HideSelectionRow.Options.UseForeColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(98, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(37, Byte), Integer))
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(230, Byte), Integer))
                gv.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.OddRow.Options.UseBackColor = True
                gv.Appearance.OddRow.Options.UseForeColor = True
                gv.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(253, Byte), Integer), CType(CType(246, Byte), Integer))
                gv.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
                gv.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(50, Byte), Integer))
                gv.Appearance.Preview.Options.UseBackColor = True
                gv.Appearance.Preview.Options.UseFont = True
                gv.Appearance.Preview.Options.UseForeColor = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(230, Byte), Integer))
                gv.Appearance.Row.ForeColor = System.Drawing.Color.Black
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.Row.Options.UseForeColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(177, Byte), Integer))
                gv.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(41, Byte), Integer))
                gv.Appearance.SelectedRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(41, Byte), Integer))
                gv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.SelectedRow.Options.UseBackColor = True
                gv.Appearance.SelectedRow.Options.UseBorderColor = True
                gv.Appearance.SelectedRow.Options.UseForeColor = True
                gv.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
                gv.Appearance.TopNewRow.Options.UseBackColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(98, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(37, Byte), Integer))
                gv.Appearance.VertLine.Options.UseBackColor = True
            ElseIf par_style = "Autumn" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
                gv.Appearance.Empty.BackColor2 = System.Drawing.Color.White
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(236, Byte), Integer))
                gv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.EvenRow.Options.UseBackColor = True
                gv.Appearance.EvenRow.Options.UseForeColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
                gv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterCloseButton.Options.UseForeColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
                gv.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(218, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(73, Byte), Integer))
                gv.Appearance.FixedLine.Options.UseBackColor = True
                gv.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
                gv.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FocusedCell.Options.UseBackColor = True
                gv.Appearance.FocusedCell.Options.UseForeColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(154, Byte), Integer), CType(CType(91, Byte), Integer))
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
                gv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.FooterPanel.Options.UseForeColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
                gv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.Options.UseForeColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
                gv.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
                gv.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
                gv.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseBorderColor = True
                gv.Appearance.GroupRow.Options.UseForeColor = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(124, Byte), Integer))
                gv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HeaderPanel.Options.UseForeColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(183, Byte), Integer), CType(CType(125, Byte), Integer))
                gv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HideSelectionRow.Options.UseForeColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(208, Byte), Integer))
                gv.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.OddRow.Options.UseBackColor = True
                gv.Appearance.OddRow.Options.UseForeColor = True
                gv.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(249, Byte), Integer))
                gv.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
                gv.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(186, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(78, Byte), Integer))
                gv.Appearance.Preview.Options.UseBackColor = True
                gv.Appearance.Preview.Options.UseFont = True
                gv.Appearance.Preview.Options.UseForeColor = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(236, Byte), Integer))
                gv.Appearance.Row.BorderColor = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(236, Byte), Integer))
                gv.Appearance.Row.ForeColor = System.Drawing.Color.Black
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.Row.Options.UseBorderColor = True
                gv.Appearance.Row.Options.UseForeColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(232, Byte), Integer))
                gv.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(103, Byte), Integer))
                gv.Appearance.SelectedRow.Options.UseBackColor = True
                gv.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
                gv.Appearance.TopNewRow.Options.UseBackColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(94, Byte), Integer))
                gv.Appearance.VertLine.Options.UseBackColor = True
            ElseIf par_style = "Brown" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(195, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(62, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(195, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(62, Byte), Integer))
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.White
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(125, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(125, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.White
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(219, Byte), Integer))
                gv.Appearance.Empty.BackColor2 = System.Drawing.Color.White
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(219, Byte), Integer))
                gv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.EvenRow.Options.UseBackColor = True
                gv.Appearance.EvenRow.Options.UseForeColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(195, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(62, Byte), Integer))
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(195, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(62, Byte), Integer))
                gv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.White
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterCloseButton.Options.UseForeColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(93, Byte), Integer))
                gv.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(88, Byte), Integer))
                gv.Appearance.FixedLine.Options.UseBackColor = True
                gv.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
                gv.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FocusedCell.Options.UseBackColor = True
                gv.Appearance.FocusedCell.Options.UseForeColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(91, Byte), Integer))
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(195, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(62, Byte), Integer))
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(195, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(62, Byte), Integer))
                gv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.FooterPanel.Options.UseForeColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(195, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(62, Byte), Integer))
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(195, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(62, Byte), Integer))
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(125, Byte), Integer))
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(125, Byte), Integer))
                gv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.Options.UseForeColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(93, Byte), Integer))
                gv.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(125, Byte), Integer))
                gv.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(125, Byte), Integer))
                gv.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseBorderColor = True
                gv.Appearance.GroupRow.Options.UseForeColor = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(93, Byte), Integer))
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(93, Byte), Integer))
                gv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HeaderPanel.Options.UseForeColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(134, Byte), Integer))
                gv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(216, Byte), Integer))
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HideSelectionRow.Options.UseForeColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(107, Byte), Integer))
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(190, Byte), Integer))
                gv.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.OddRow.Options.UseBackColor = True
                gv.Appearance.OddRow.Options.UseForeColor = True
                gv.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(237, Byte), Integer))
                gv.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
                gv.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(88, Byte), Integer))
                gv.Appearance.Preview.Options.UseBackColor = True
                gv.Appearance.Preview.Options.UseFont = True
                gv.Appearance.Preview.Options.UseForeColor = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(219, Byte), Integer))
                gv.Appearance.Row.ForeColor = System.Drawing.Color.Black
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.Row.Options.UseForeColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(219, Byte), Integer))
                gv.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(187, Byte), Integer), CType(CType(159, Byte), Integer), CType(CType(114, Byte), Integer))
                gv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.SelectedRow.Options.UseBackColor = True
                gv.Appearance.SelectedRow.Options.UseForeColor = True
                gv.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
                gv.Appearance.TopNewRow.Options.UseBackColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(107, Byte), Integer))
                gv.Appearance.VertLine.Options.UseBackColor = True
            ElseIf par_style = "Chiaroscuro" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(66, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(66, Byte), Integer))
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(95, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(95, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
                gv.Appearance.Empty.BackColor2 = System.Drawing.Color.White
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(248, Byte), Integer))
                gv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.EvenRow.Options.UseBackColor = True
                gv.Appearance.EvenRow.Options.UseForeColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(66, Byte), Integer))
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(66, Byte), Integer))
                gv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterCloseButton.Options.UseForeColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
                gv.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
                gv.Appearance.FixedLine.Options.UseBackColor = True
                gv.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
                gv.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FocusedCell.Options.UseBackColor = True
                gv.Appearance.FocusedCell.Options.UseForeColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(194, Byte), Integer))
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(66, Byte), Integer))
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(66, Byte), Integer))
                gv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.FooterPanel.Options.UseForeColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(66, Byte), Integer))
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(66, Byte), Integer))
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(95, Byte), Integer))
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(95, Byte), Integer))
                gv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.Options.UseForeColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
                gv.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(95, Byte), Integer))
                gv.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(95, Byte), Integer))
                gv.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseBorderColor = True
                gv.Appearance.GroupRow.Options.UseForeColor = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(95, Byte), Integer))
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(95, Byte), Integer))
                gv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HeaderPanel.Options.UseForeColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gainsboro
                gv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HideSelectionRow.Options.UseForeColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(175, Byte), Integer), CType(CType(175, Byte), Integer), CType(CType(175, Byte), Integer))
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
                gv.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
                gv.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.OddRow.Options.UseBackColor = True
                gv.Appearance.OddRow.Options.UseBorderColor = True
                gv.Appearance.OddRow.Options.UseForeColor = True
                gv.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
                gv.Appearance.Preview.Font = New System.Drawing.Font("Verdana", 7.5!)
                gv.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
                gv.Appearance.Preview.Options.UseBackColor = True
                gv.Appearance.Preview.Options.UseFont = True
                gv.Appearance.Preview.Options.UseForeColor = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
                gv.Appearance.Row.BorderColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
                gv.Appearance.Row.ForeColor = System.Drawing.Color.Black
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.Row.Options.UseBorderColor = True
                gv.Appearance.Row.Options.UseForeColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
                gv.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer))
                gv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.SelectedRow.Options.UseBackColor = True
                gv.Appearance.SelectedRow.Options.UseForeColor = True
                gv.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
                gv.Appearance.TopNewRow.Options.UseBackColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(175, Byte), Integer), CType(CType(175, Byte), Integer), CType(CType(175, Byte), Integer))
                gv.Appearance.VertLine.Options.UseBackColor = True
            ElseIf par_style = "Desert" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(187, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(207, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(187, Byte), Integer))
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Gray
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(207, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(219, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(207, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Blue
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(207, Byte), Integer))
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(187, Byte), Integer))
                gv.Appearance.EvenRow.BackColor2 = System.Drawing.Color.GhostWhite
                gv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.EvenRow.Options.UseBackColor = True
                gv.Appearance.EvenRow.Options.UseForeColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(201, Byte), Integer), CType(CType(164, Byte), Integer))
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterCloseButton.Options.UseForeColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(74, Byte), Integer))
                gv.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(92, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(34, Byte), Integer))
                gv.Appearance.FixedLine.Options.UseBackColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.Teal
                gv.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(178, Byte), Integer))
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(187, Byte), Integer))
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(187, Byte), Integer))
                gv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.FooterPanel.Options.UseForeColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(187, Byte), Integer))
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(187, Byte), Integer))
                gv.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupButton.Options.UseForeColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(197, Byte), Integer))
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(197, Byte), Integer))
                gv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.Options.UseForeColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(162, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(104, Byte), Integer))
                gv.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.GroupPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseFont = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(162, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(104, Byte), Integer))
                gv.Appearance.GroupRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseForeColor = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(187, Byte), Integer))
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(187, Byte), Integer))
                gv.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
                gv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HeaderPanel.Options.UseFont = True
                gv.Appearance.HeaderPanel.Options.UseForeColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray
                gv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HideSelectionRow.Options.UseForeColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(187, Byte), Integer))
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(221, Byte), Integer))
                gv.Appearance.OddRow.BackColor2 = System.Drawing.Color.White
                gv.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
                gv.Appearance.OddRow.Options.UseBackColor = True
                gv.Appearance.OddRow.Options.UseForeColor = True
                gv.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(247, Byte), Integer))
                gv.Appearance.Preview.BackColor2 = System.Drawing.Color.White
                gv.Appearance.Preview.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
                gv.Appearance.Preview.ForeColor = System.Drawing.Color.Teal
                gv.Appearance.Preview.Options.UseBackColor = True
                gv.Appearance.Preview.Options.UseFont = True
                gv.Appearance.Preview.Options.UseForeColor = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.White
                gv.Appearance.Row.ForeColor = System.Drawing.Color.Black
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.Row.Options.UseForeColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
                gv.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(207, Byte), Integer))
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
                gv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.SelectedRow.Options.UseBackColor = True
                gv.Appearance.SelectedRow.Options.UseForeColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(187, Byte), Integer))
                gv.Appearance.VertLine.Options.UseBackColor = True
            ElseIf par_style = "Gray" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.DarkGray
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.DarkGray
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.DimGray
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.DarkGray
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.DarkGray
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Gainsboro
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.DimGray
                gv.Appearance.Empty.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.EvenRow.BackColor = System.Drawing.Color.White
                gv.Appearance.EvenRow.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.Gray
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.Gray
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.Gray
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.Black
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.DarkGray
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.DarkGray
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.Silver
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.Silver
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.Silver
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.Silver
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.DimGray
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.Silver
                gv.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseFont = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.DarkGray
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.DarkGray
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.LightSlateGray
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.LightGray
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.OddRow.BackColor = System.Drawing.Color.WhiteSmoke
                gv.Appearance.OddRow.Options.UseBackColor = True
                gv.Appearance.Preview.BackColor = System.Drawing.Color.Gainsboro
                gv.Appearance.Preview.ForeColor = System.Drawing.Color.DimGray
                gv.Appearance.Preview.Options.UseBackColor = True
                gv.Appearance.Preview.Options.UseForeColor = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.White
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.DimGray
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.SelectedRow.BackColor = System.Drawing.Color.DimGray
                gv.Appearance.SelectedRow.Options.UseBackColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.LightGray
                gv.Appearance.VertLine.Options.UseBackColor = True
            ElseIf par_style = "Orange" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.Orange
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.Orange
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.DarkOrange
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.DarkOrange
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.DarkOrange
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.White
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.LightSkyBlue
                gv.Appearance.Empty.BackColor2 = System.Drawing.Color.SkyBlue
                gv.Appearance.Empty.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.EvenRow.BackColor = System.Drawing.Color.Linen
                gv.Appearance.EvenRow.BackColor2 = System.Drawing.Color.AntiqueWhite
                gv.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.EvenRow.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.Orange
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.Orange
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.DarkOrange
                gv.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.Orange
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.RoyalBlue
                gv.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.FocusedRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.Orange
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.Orange
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.Wheat
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.Wheat
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.Wheat
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.Wheat
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.RoyalBlue
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.Wheat
                gv.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseFont = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Orange
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.Orange
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.LightSlateGray
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.Tan
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.OddRow.BackColor = System.Drawing.Color.White
                gv.Appearance.OddRow.Options.UseBackColor = True
                gv.Appearance.Preview.BackColor = System.Drawing.Color.Khaki
                gv.Appearance.Preview.BackColor2 = System.Drawing.Color.Cornsilk
                gv.Appearance.Preview.Font = New System.Drawing.Font("Tahoma", 7.5!)
                gv.Appearance.Preview.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
                gv.Appearance.Preview.Options.UseBackColor = True
                gv.Appearance.Preview.Options.UseFont = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.White
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.LightSkyBlue
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.Tan
                gv.Appearance.VertLine.Options.UseBackColor = True
            ElseIf par_style = "Blue Office" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(228, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.ColumnFilterButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(154, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(243, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
                gv.Appearance.ColumnFilterButtonActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.White
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(254, Byte), Integer))
                gv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.EvenRow.Options.UseBackColor = True
                gv.Appearance.EvenRow.Options.UseForeColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
                gv.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(228, Byte), Integer))
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
                gv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterCloseButton.Options.UseForeColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(185, Byte), Integer))
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(156, Byte), Integer))
                gv.Appearance.FixedLine.Options.UseBackColor = True
                gv.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
                gv.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FocusedCell.Options.UseBackColor = True
                gv.Appearance.FocusedCell.Options.UseForeColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(197, Byte), Integer))
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
                gv.Appearance.FooterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(228, Byte), Integer))
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
                gv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.FooterPanel.Options.UseForeColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(247, Byte), Integer))
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(247, Byte), Integer))
                gv.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupButton.Options.UseForeColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(247, Byte), Integer))
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(247, Byte), Integer))
                gv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.Options.UseForeColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(185, Byte), Integer))
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(247, Byte), Integer))
                gv.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(247, Byte), Integer))
                gv.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
                gv.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseBorderColor = True
                gv.Appearance.GroupRow.Options.UseFont = True
                gv.Appearance.GroupRow.Options.UseForeColor = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
                gv.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(228, Byte), Integer))
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
                gv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HeaderPanel.Options.UseForeColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(106, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(228, Byte), Integer))
                gv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(251, Byte), Integer))
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HideSelectionRow.Options.UseForeColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(99, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(196, Byte), Integer))
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.OddRow.BackColor = System.Drawing.Color.White
                gv.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.OddRow.Options.UseBackColor = True
                gv.Appearance.OddRow.Options.UseForeColor = True
                gv.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(255, Byte), Integer))
                gv.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(129, Byte), Integer), CType(CType(185, Byte), Integer))
                gv.Appearance.Preview.Options.UseBackColor = True
                gv.Appearance.Preview.Options.UseForeColor = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.White
                gv.Appearance.Row.ForeColor = System.Drawing.Color.Black
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.Row.Options.UseForeColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(217, Byte), Integer))
                gv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.SelectedRow.Options.UseBackColor = True
                gv.Appearance.SelectedRow.Options.UseForeColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(99, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(196, Byte), Integer))
                gv.Appearance.VertLine.Options.UseBackColor = True
            ElseIf par_style = "Olive Office" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(222, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(184, Byte), Integer), CType(CType(199, Byte), Integer), CType(CType(146, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(222, Byte), Integer))
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.ColumnFilterButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(244, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(183, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(244, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
                gv.Appearance.ColumnFilterButtonActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.White
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(222, Byte), Integer))
                gv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.EvenRow.Options.UseBackColor = True
                gv.Appearance.EvenRow.Options.UseForeColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(222, Byte), Integer))
                gv.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(184, Byte), Integer), CType(CType(199, Byte), Integer), CType(CType(146, Byte), Integer))
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(222, Byte), Integer))
                gv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterCloseButton.Options.UseForeColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(126, Byte), Integer))
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(96, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(88, Byte), Integer))
                gv.Appearance.FixedLine.Options.UseBackColor = True
                gv.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
                gv.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FocusedCell.Options.UseBackColor = True
                gv.Appearance.FocusedCell.Options.UseForeColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(147, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(112, Byte), Integer))
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(222, Byte), Integer))
                gv.Appearance.FooterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(184, Byte), Integer), CType(CType(199, Byte), Integer), CType(CType(146, Byte), Integer))
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(222, Byte), Integer))
                gv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.FooterPanel.Options.UseForeColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(195, Byte), Integer))
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(195, Byte), Integer))
                gv.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupButton.Options.UseForeColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(195, Byte), Integer))
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(195, Byte), Integer))
                gv.Appearance.GroupFooter.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
                gv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.Options.UseFont = True
                gv.Appearance.GroupFooter.Options.UseForeColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(139, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(126, Byte), Integer))
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(222, Byte), Integer))
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(195, Byte), Integer))
                gv.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(195, Byte), Integer))
                gv.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
                gv.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseBorderColor = True
                gv.Appearance.GroupRow.Options.UseFont = True
                gv.Appearance.GroupRow.Options.UseForeColor = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(222, Byte), Integer))
                gv.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(184, Byte), Integer), CType(CType(199, Byte), Integer), CType(CType(146, Byte), Integer))
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(222, Byte), Integer))
                gv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HeaderPanel.Options.UseForeColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(170, Byte), Integer))
                gv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(147, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(112, Byte), Integer))
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HideSelectionRow.Options.UseForeColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(128, Byte), Integer))
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(253, Byte), Integer), CType(CType(247, Byte), Integer))
                gv.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(147, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(112, Byte), Integer))
                gv.Appearance.Preview.Options.UseBackColor = True
                gv.Appearance.Preview.Options.UseForeColor = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.White
                gv.Appearance.Row.ForeColor = System.Drawing.Color.Black
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.Row.Options.UseForeColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(133, Byte), Integer))
                gv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.SelectedRow.Options.UseBackColor = True
                gv.Appearance.SelectedRow.Options.UseForeColor = True
                gv.Appearance.TopNewRow.BackColor = System.Drawing.Color.White
                gv.Appearance.TopNewRow.Options.UseBackColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(188, Byte), Integer))
                gv.Appearance.VertLine.Options.UseBackColor = True
            ElseIf par_style = "Silver Office" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(250, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(154, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(182, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(250, Byte), Integer))
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.ColumnFilterButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.White
                gv.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(215, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.White
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
                gv.Appearance.ColumnFilterButtonActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.White
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(250, Byte), Integer))
                gv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.EvenRow.Options.UseBackColor = True
                gv.Appearance.EvenRow.Options.UseForeColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(250, Byte), Integer))
                gv.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(154, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(182, Byte), Integer))
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(250, Byte), Integer))
                gv.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(161, Byte), Integer))
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(124, Byte), Integer), CType(CType(148, Byte), Integer))
                gv.Appearance.FixedLine.Options.UseBackColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(178, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(191, Byte), Integer))
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(250, Byte), Integer))
                gv.Appearance.FooterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(154, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(182, Byte), Integer))
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(250, Byte), Integer))
                gv.Appearance.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
                gv.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupButton.Options.UseForeColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
                gv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.Options.UseForeColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(161, Byte), Integer))
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(250, Byte), Integer))
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
                gv.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
                gv.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
                gv.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseBorderColor = True
                gv.Appearance.GroupRow.Options.UseFont = True
                gv.Appearance.GroupRow.Options.UseForeColor = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(250, Byte), Integer))
                gv.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(154, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(182, Byte), Integer))
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(250, Byte), Integer))
                gv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HeaderPanel.Options.UseForeColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(218, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(226, Byte), Integer))
                gv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(131, Byte), Integer), CType(CType(161, Byte), Integer))
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HideSelectionRow.Options.UseForeColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(164, Byte), Integer), CType(CType(188, Byte), Integer))
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.OddRow.BackColor = System.Drawing.Color.White
                gv.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.OddRow.Options.UseBackColor = True
                gv.Appearance.OddRow.Options.UseForeColor = True
                gv.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(253, Byte), Integer))
                gv.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(163, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(177, Byte), Integer))
                gv.Appearance.Preview.Options.UseBackColor = True
                gv.Appearance.Preview.Options.UseForeColor = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.White
                gv.Appearance.Row.ForeColor = System.Drawing.Color.Black
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.Row.Options.UseForeColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(195, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(205, Byte), Integer))
                gv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.SelectedRow.Options.UseBackColor = True
                gv.Appearance.SelectedRow.Options.UseForeColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(164, Byte), Integer), CType(CType(164, Byte), Integer), CType(CType(188, Byte), Integer))
                gv.Appearance.VertLine.Options.UseBackColor = True
            ElseIf par_style = "UserFormat1" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.BurlyWood
                gv.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(155, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.BurlyWood
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Gray
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(155, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(218, Byte), Integer), CType(CType(180, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(155, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Blue
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(250, Byte), Integer))
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(159, Byte), Integer), CType(CType(109, Byte), Integer))
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterCloseButton.Options.UseForeColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.SaddleBrown
                gv.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(185, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(115, Byte), Integer))
                gv.Appearance.FixedLine.Options.UseBackColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.Navy
                gv.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(228, Byte), Integer))
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.BurlyWood
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.BurlyWood
                gv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.FooterPanel.Options.UseForeColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(195, Byte), Integer))
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(195, Byte), Integer))
                gv.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupButton.Options.UseForeColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(145, Byte), Integer))
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(145, Byte), Integer))
                gv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.Options.UseForeColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.SaddleBrown
                gv.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.GroupPanel.Font = New System.Drawing.Font("Tahoma", 9.0!)
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseFont = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.PeachPuff
                gv.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseForeColor = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.BurlyWood
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.BurlyWood
                gv.Appearance.HeaderPanel.Font = New System.Drawing.Font("Arial Narrow", 8.0!, System.Drawing.FontStyle.Bold)
                gv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HeaderPanel.Options.UseFont = True
                gv.Appearance.HeaderPanel.Options.UseForeColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray
                gv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HideSelectionRow.Options.UseForeColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.Tan
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.Preview.BackColor = System.Drawing.Color.White
                gv.Appearance.Preview.ForeColor = System.Drawing.Color.SaddleBrown
                gv.Appearance.Preview.Options.UseBackColor = True
                gv.Appearance.Preview.Options.UseForeColor = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.OldLace
                gv.Appearance.Row.ForeColor = System.Drawing.Color.MidnightBlue
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.Row.Options.UseForeColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
                gv.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(250, Byte), Integer))
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(138, Byte), Integer))
                gv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.SelectedRow.Options.UseBackColor = True
                gv.Appearance.SelectedRow.Options.UseForeColor = True
                gv.Appearance.TopNewRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(236, Byte), Integer))
                gv.Appearance.TopNewRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(102, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(221, Byte), Integer))
                gv.Appearance.TopNewRow.Options.UseBackColor = True
                gv.Appearance.TopNewRow.Options.UseForeColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.Tan
                gv.Appearance.VertLine.Options.UseBackColor = True
            ElseIf par_style = "UserFormat2" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.DarkSeaGreen
                gv.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(163, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(159, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.DarkSeaGreen
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Gray
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(163, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(159, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(187, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(184, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(163, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(159, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Blue
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(160, Byte), Integer))
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.EvenRow.BackColor = System.Drawing.Color.Khaki
                gv.Appearance.EvenRow.BackColor2 = System.Drawing.Color.GhostWhite
                gv.Appearance.EvenRow.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Bold)
                gv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.EvenRow.Options.UseBackColor = True
                gv.Appearance.EvenRow.Options.UseFont = True
                gv.Appearance.EvenRow.Options.UseForeColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(106, Byte), Integer), CType(CType(199, Byte), Integer), CType(CType(147, Byte), Integer))
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterCloseButton.Options.UseForeColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(57, Byte), Integer))
                gv.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(100, Byte), Integer))
                gv.Appearance.FixedLine.Options.UseBackColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.SeaGreen
                gv.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(187, Byte), Integer))
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.DarkSeaGreen
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.DarkSeaGreen
                gv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.FooterPanel.Options.UseForeColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(112, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(180, Byte), Integer))
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(112, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(180, Byte), Integer))
                gv.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupButton.Options.UseForeColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(153, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(149, Byte), Integer))
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(153, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(149, Byte), Integer))
                gv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.Options.UseForeColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.SeaGreen
                gv.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.GroupPanel.Font = New System.Drawing.Font("Arial Black", 8.0!)
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseFont = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.MediumAquamarine
                gv.Appearance.GroupRow.Font = New System.Drawing.Font("Times New Roman", 9.0!)
                gv.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseFont = True
                gv.Appearance.GroupRow.Options.UseForeColor = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.DarkSeaGreen
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.DarkSeaGreen
                gv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HeaderPanel.Options.UseForeColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray
                gv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HideSelectionRow.Options.UseForeColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.OddRow.BackColor = System.Drawing.Color.Wheat
                gv.Appearance.OddRow.BackColor2 = System.Drawing.Color.White
                gv.Appearance.OddRow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Italic)
                gv.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
                gv.Appearance.OddRow.Options.UseBackColor = True
                gv.Appearance.OddRow.Options.UseFont = True
                gv.Appearance.OddRow.Options.UseForeColor = True
                gv.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.Preview.BackColor2 = System.Drawing.Color.White
                gv.Appearance.Preview.ForeColor = System.Drawing.Color.DarkGreen
                gv.Appearance.Preview.Options.UseBackColor = True
                gv.Appearance.Preview.Options.UseForeColor = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.White
                gv.Appearance.Row.ForeColor = System.Drawing.Color.Black
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.Row.Options.UseForeColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
                gv.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(160, Byte), Integer))
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(56, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(97, Byte), Integer))
                gv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.SelectedRow.Options.UseBackColor = True
                gv.Appearance.SelectedRow.Options.UseForeColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.Gray
                gv.Appearance.VertLine.Options.UseBackColor = True
            ElseIf par_style = "UserFormat3" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.DarkTurquoise
                gv.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(229, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.DarkTurquoise
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Gray
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(229, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(239, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(229, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Blue
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.White
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.EvenRow.BackColor = System.Drawing.Color.GhostWhite
                gv.Appearance.EvenRow.BackColor2 = System.Drawing.Color.GhostWhite
                gv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.EvenRow.Options.UseBackColor = True
                gv.Appearance.EvenRow.Options.UseForeColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(220, Byte), Integer))
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterCloseButton.Options.UseForeColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.Indigo
                gv.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Cornsilk
                gv.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(106, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(152, Byte), Integer))
                gv.Appearance.FixedLine.Options.UseBackColor = True
                gv.Appearance.FocusedCell.BackColor = System.Drawing.Color.MistyRose
                gv.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FocusedCell.Options.UseBackColor = True
                gv.Appearance.FocusedCell.Options.UseForeColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.Red
                gv.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.DarkTurquoise
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.DarkTurquoise
                gv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.FooterPanel.Options.UseForeColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(186, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(232, Byte), Integer))
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(186, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(232, Byte), Integer))
                gv.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupButton.Options.UseForeColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(219, Byte), Integer))
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(219, Byte), Integer))
                gv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.Options.UseForeColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.Indigo
                gv.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Cornsilk
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.LightSteelBlue
                gv.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseForeColor = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.DarkTurquoise
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.DarkTurquoise
                gv.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
                gv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HeaderPanel.Options.UseFont = True
                gv.Appearance.HeaderPanel.Options.UseForeColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray
                gv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HideSelectionRow.Options.UseForeColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.Gray
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.OddRow.BackColor = System.Drawing.Color.Lavender
                gv.Appearance.OddRow.BackColor2 = System.Drawing.Color.White
                gv.Appearance.OddRow.Font = New System.Drawing.Font("Arial Black", 7.0!)
                gv.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
                gv.Appearance.OddRow.Options.UseBackColor = True
                gv.Appearance.OddRow.Options.UseFont = True
                gv.Appearance.OddRow.Options.UseForeColor = True
                gv.Appearance.Preview.BackColor = System.Drawing.Color.White
                gv.Appearance.Preview.ForeColor = System.Drawing.Color.SteelBlue
                gv.Appearance.Preview.Options.UseBackColor = True
                gv.Appearance.Preview.Options.UseForeColor = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.White
                gv.Appearance.Row.ForeColor = System.Drawing.Color.Black
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.Row.Options.UseForeColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
                gv.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer))
                gv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.SelectedRow.Options.UseBackColor = True
                gv.Appearance.SelectedRow.Options.UseForeColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.Gray
                gv.Appearance.VertLine.Options.UseBackColor = True
            ElseIf par_style = "UserFormat4" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.DimGray
                gv.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.DarkGray
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.DimGray
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Silver
                gv.Appearance.ColumnFilterButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.DarkGray
                gv.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.Silver
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.DarkGray
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Blue
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(189, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(189, Byte), Integer))
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.EvenRow.BackColor = System.Drawing.Color.DarkGray
                gv.Appearance.EvenRow.BackColor2 = System.Drawing.Color.GhostWhite
                gv.Appearance.EvenRow.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
                gv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.EvenRow.Options.UseBackColor = True
                gv.Appearance.EvenRow.Options.UseFont = True
                gv.Appearance.EvenRow.Options.UseForeColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer))
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterCloseButton.Options.UseForeColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(162, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(162, Byte), Integer))
                gv.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
                gv.Appearance.FixedLine.Options.UseBackColor = True
                gv.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
                gv.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FocusedCell.Options.UseBackColor = True
                gv.Appearance.FocusedCell.Options.UseForeColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.Black
                gv.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.DimGray
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.DimGray
                gv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.FooterPanel.Options.UseForeColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
                gv.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupButton.Options.UseForeColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(115, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(115, Byte), Integer))
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(115, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(115, Byte), Integer))
                gv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.White
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.Options.UseForeColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.Silver
                gv.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.Gray
                gv.Appearance.GroupRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseForeColor = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.DimGray
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.DimGray
                gv.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
                gv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HeaderPanel.Options.UseFont = True
                gv.Appearance.HeaderPanel.Options.UseForeColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray
                gv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HideSelectionRow.Options.UseForeColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.Black
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(229, Byte), Integer))
                gv.Appearance.Preview.BackColor2 = System.Drawing.Color.White
                gv.Appearance.Preview.ForeColor = System.Drawing.Color.DimGray
                gv.Appearance.Preview.Options.UseBackColor = True
                gv.Appearance.Preview.Options.UseForeColor = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.White
                gv.Appearance.Row.ForeColor = System.Drawing.Color.Black
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.Row.Options.UseForeColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
                gv.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(189, Byte), Integer), CType(CType(189, Byte), Integer), CType(CType(189, Byte), Integer))
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer))
                gv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.SelectedRow.Options.UseBackColor = True
                gv.Appearance.SelectedRow.Options.UseForeColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.Black
                gv.Appearance.VertLine.Options.UseBackColor = True
            ElseIf par_style = "Rose" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(175, Byte), Integer), CType(CType(183, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(203, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(175, Byte), Integer), CType(CType(183, Byte), Integer))
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Gray
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(203, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(217, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(203, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Blue
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(203, Byte), Integer))
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(188, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(188, Byte), Integer))
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterCloseButton.Options.UseForeColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(98, Byte), Integer), CType(CType(98, Byte), Integer), CType(CType(98, Byte), Integer))
                gv.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(26, Byte), Integer), CType(CType(42, Byte), Integer))
                gv.Appearance.FixedLine.Options.UseBackColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(112, Byte), Integer))
                gv.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(209, Byte), Integer), CType(CType(146, Byte), Integer), CType(CType(162, Byte), Integer))
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(175, Byte), Integer), CType(CType(183, Byte), Integer))
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(175, Byte), Integer), CType(CType(183, Byte), Integer))
                gv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.FooterPanel.Options.UseForeColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(175, Byte), Integer), CType(CType(183, Byte), Integer))
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(175, Byte), Integer), CType(CType(183, Byte), Integer))
                gv.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupButton.Options.UseForeColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(193, Byte), Integer))
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(193, Byte), Integer))
                gv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.Options.UseForeColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.Gray
                gv.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.GroupPanel.Font = New System.Drawing.Font("Times New Roman", 10.0!)
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseFont = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(112, Byte), Integer))
                gv.Appearance.GroupRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(221, Byte), Integer))
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseForeColor = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(175, Byte), Integer), CType(CType(183, Byte), Integer))
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(175, Byte), Integer), CType(CType(183, Byte), Integer))
                gv.Appearance.HeaderPanel.Font = New System.Drawing.Font("Times New Roman", 10.0!)
                gv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HeaderPanel.Options.UseFont = True
                gv.Appearance.HeaderPanel.Options.UseForeColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray
                gv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HideSelectionRow.Options.UseForeColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(175, Byte), Integer), CType(CType(183, Byte), Integer))
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(243, Byte), Integer))
                gv.Appearance.Preview.BackColor2 = System.Drawing.Color.White
                gv.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(112, Byte), Integer))
                gv.Appearance.Preview.Options.UseBackColor = True
                gv.Appearance.Preview.Options.UseForeColor = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.White
                gv.Appearance.Row.ForeColor = System.Drawing.Color.Black
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.Row.Options.UseForeColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
                gv.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(203, Byte), Integer))
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(122, Byte), Integer))
                gv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.SelectedRow.Options.UseBackColor = True
                gv.Appearance.SelectedRow.Options.UseForeColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(175, Byte), Integer), CType(CType(183, Byte), Integer))
                gv.Appearance.VertLine.Options.UseBackColor = True
            ElseIf par_style = "Windows Classic" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.Silver
                gv.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.Silver
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Gray
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Blue
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer))
                gv.Appearance.EvenRow.BackColor2 = System.Drawing.Color.GhostWhite
                gv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.EvenRow.Options.UseBackColor = True
                gv.Appearance.EvenRow.Options.UseForeColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(118, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(225, Byte), Integer))
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterCloseButton.Options.UseForeColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(135, Byte), Integer))
                gv.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
                gv.Appearance.FixedLine.Options.UseBackColor = True
                gv.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(225, Byte), Integer))
                gv.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FocusedCell.Options.UseBackColor = True
                gv.Appearance.FocusedCell.Options.UseForeColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.Navy
                gv.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(178, Byte), Integer))
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.Silver
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.Silver
                gv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.FooterPanel.Options.UseForeColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.Silver
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.Silver
                gv.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupButton.Options.UseForeColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
                gv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.Options.UseForeColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(165, Byte), Integer))
                gv.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.GroupPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseFont = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.Gray
                gv.Appearance.GroupRow.ForeColor = System.Drawing.Color.Silver
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseForeColor = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Silver
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.Silver
                gv.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
                gv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HeaderPanel.Options.UseFont = True
                gv.Appearance.HeaderPanel.Options.UseForeColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray
                gv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HideSelectionRow.Options.UseForeColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.Silver
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.OddRow.BackColor = System.Drawing.Color.White
                gv.Appearance.OddRow.BackColor2 = System.Drawing.Color.White
                gv.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
                gv.Appearance.OddRow.Options.UseBackColor = True
                gv.Appearance.OddRow.Options.UseForeColor = True
                gv.Appearance.Preview.BackColor = System.Drawing.Color.White
                gv.Appearance.Preview.ForeColor = System.Drawing.Color.Navy
                gv.Appearance.Preview.Options.UseBackColor = True
                gv.Appearance.Preview.Options.UseForeColor = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.White
                gv.Appearance.Row.ForeColor = System.Drawing.Color.Black
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.Row.Options.UseForeColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
                gv.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(138, Byte), Integer))
                gv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.SelectedRow.Options.UseBackColor = True
                gv.Appearance.SelectedRow.Options.UseForeColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.Silver
                gv.Appearance.VertLine.Options.UseBackColor = True
            ElseIf par_style = "Windows Standard" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(220, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Gray
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(220, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(230, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(220, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Blue
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(220, Byte), Integer))
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.EvenRow.BackColor2 = System.Drawing.Color.GhostWhite
                gv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.EvenRow.Options.UseBackColor = True
                gv.Appearance.EvenRow.Options.UseForeColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(118, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(225, Byte), Integer))
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterCloseButton.Options.UseForeColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(135, Byte), Integer))
                gv.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
                gv.Appearance.FixedLine.Options.UseBackColor = True
                gv.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(225, Byte), Integer))
                gv.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FocusedCell.Options.UseBackColor = True
                gv.Appearance.FocusedCell.Options.UseForeColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(106, Byte), Integer))
                gv.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(86, Byte), Integer), CType(CType(156, Byte), Integer))
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.FooterPanel.Options.UseForeColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupButton.Options.UseForeColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(218, Byte), Integer), CType(CType(210, Byte), Integer))
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(218, Byte), Integer), CType(CType(210, Byte), Integer))
                gv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.Options.UseForeColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(165, Byte), Integer))
                gv.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.GroupPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseFont = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.Gray
                gv.Appearance.GroupRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseForeColor = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
                gv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HeaderPanel.Options.UseFont = True
                gv.Appearance.HeaderPanel.Options.UseForeColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray
                gv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HideSelectionRow.Options.UseForeColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.OddRow.BackColor = System.Drawing.Color.White
                gv.Appearance.OddRow.BackColor2 = System.Drawing.Color.White
                gv.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
                gv.Appearance.OddRow.Options.UseBackColor = True
                gv.Appearance.OddRow.Options.UseForeColor = True
                gv.Appearance.Preview.BackColor = System.Drawing.Color.White
                gv.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(106, Byte), Integer))
                gv.Appearance.Preview.Options.UseBackColor = True
                gv.Appearance.Preview.Options.UseForeColor = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.White
                gv.Appearance.Row.ForeColor = System.Drawing.Color.Black
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.Row.Options.UseForeColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
                gv.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(220, Byte), Integer))
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(116, Byte), Integer))
                gv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.SelectedRow.Options.UseBackColor = True
                gv.Appearance.SelectedRow.Options.UseForeColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.VertLine.Options.UseBackColor = True
            ElseIf par_style = "Slate" Then
                gv.Appearance.Reset()
                gv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(220, Byte), Integer))
                gv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Gray
                gv.Appearance.ColumnFilterButton.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButton.Options.UseForeColor = True
                gv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(220, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(218, Byte), Integer), CType(CType(228, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(220, Byte), Integer))
                gv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Blue
                gv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
                gv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
                gv.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(220, Byte), Integer))
                gv.Appearance.Empty.Options.UseBackColor = True
                gv.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.EvenRow.BackColor2 = System.Drawing.Color.GhostWhite
                gv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.EvenRow.Options.UseBackColor = True
                gv.Appearance.EvenRow.Options.UseForeColor = True
                gv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(125, Byte), Integer))
                gv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.FilterCloseButton.Options.UseBackColor = True
                gv.Appearance.FilterCloseButton.Options.UseBorderColor = True
                gv.Appearance.FilterCloseButton.Options.UseForeColor = True
                gv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
                gv.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
                gv.Appearance.FilterPanel.Options.UseBackColor = True
                gv.Appearance.FilterPanel.Options.UseForeColor = True
                gv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(81, Byte), Integer))
                gv.Appearance.FixedLine.Options.UseBackColor = True
                gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(151, Byte), Integer))
                gv.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(135, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(201, Byte), Integer))
                gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.FocusedRow.Options.UseBackColor = True
                gv.Appearance.FocusedRow.Options.UseForeColor = True
                gv.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.FooterPanel.Options.UseBackColor = True
                gv.Appearance.FooterPanel.Options.UseBorderColor = True
                gv.Appearance.FooterPanel.Options.UseForeColor = True
                gv.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupButton.Options.UseBackColor = True
                gv.Appearance.GroupButton.Options.UseBorderColor = True
                gv.Appearance.GroupButton.Options.UseForeColor = True
                gv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(167, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(210, Byte), Integer))
                gv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(167, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(210, Byte), Integer))
                gv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
                gv.Appearance.GroupFooter.Options.UseBackColor = True
                gv.Appearance.GroupFooter.Options.UseBorderColor = True
                gv.Appearance.GroupFooter.Options.UseForeColor = True
                gv.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(65, Byte), Integer))
                gv.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
                gv.Appearance.GroupPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
                gv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White
                gv.Appearance.GroupPanel.Options.UseBackColor = True
                gv.Appearance.GroupPanel.Options.UseFont = True
                gv.Appearance.GroupPanel.Options.UseForeColor = True
                gv.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(151, Byte), Integer))
                gv.Appearance.GroupRow.ForeColor = System.Drawing.Color.Silver
                gv.Appearance.GroupRow.Options.UseBackColor = True
                gv.Appearance.GroupRow.Options.UseForeColor = True
                gv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
                gv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
                gv.Appearance.HeaderPanel.Options.UseBackColor = True
                gv.Appearance.HeaderPanel.Options.UseBorderColor = True
                gv.Appearance.HeaderPanel.Options.UseFont = True
                gv.Appearance.HeaderPanel.Options.UseForeColor = True
                gv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray
                gv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.HideSelectionRow.Options.UseBackColor = True
                gv.Appearance.HideSelectionRow.Options.UseForeColor = True
                gv.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.HorzLine.Options.UseBackColor = True
                gv.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(206, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(227, Byte), Integer))
                gv.Appearance.OddRow.BackColor2 = System.Drawing.Color.White
                gv.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
                gv.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
                gv.Appearance.OddRow.Options.UseBackColor = True
                gv.Appearance.OddRow.Options.UseForeColor = True
                gv.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(255, Byte), Integer))
                gv.Appearance.Preview.BackColor2 = System.Drawing.Color.White
                gv.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(85, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(151, Byte), Integer))
                gv.Appearance.Preview.Options.UseBackColor = True
                gv.Appearance.Preview.Options.UseForeColor = True
                gv.Appearance.Row.BackColor = System.Drawing.Color.White
                gv.Appearance.Row.ForeColor = System.Drawing.Color.Black
                gv.Appearance.Row.Options.UseBackColor = True
                gv.Appearance.Row.Options.UseForeColor = True
                gv.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
                gv.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(177, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(220, Byte), Integer))
                gv.Appearance.RowSeparator.Options.UseBackColor = True
                gv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(95, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(161, Byte), Integer))
                gv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
                gv.Appearance.SelectedRow.Options.UseBackColor = True
                gv.Appearance.SelectedRow.Options.UseForeColor = True
                gv.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(200, Byte), Integer))
                gv.Appearance.VertLine.Options.UseBackColor = True
            End If
        End If
    End Sub

    Public Overrides Function get_gv() As Object
        get_gv = DBNull.Value

        'Dim active_page As Integer
        Dim active_page_name As String

        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_sgp, DevExpress.XtraTab.XtraTabControl)
                                'active_page = xtc.SelectedTabPageIndex
                                active_page_name = xtc.SelectedTabPage.Name
                                For Each ctrl_xtc In xtc.Controls
                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                        If xtp.Name = active_page_name Then
                                            For Each ctrl_xtp In xtp.Controls
                                                If TypeOf ctrl_xtp Is DevExpress.XtraGrid.GridControl Then
                                                    gc = CType(ctrl_xtp, DevExpress.XtraGrid.GridControl)
                                                    gv = gc.MainView
                                                    get_gv = gv
                                                ElseIf TypeOf ctrl_xtp Is DevExpress.XtraPivotGrid.PivotGridControl Then
                                                    get_gv = CType(ctrl_xtp, DevExpress.XtraPivotGrid.PivotGridControl)
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
        Return get_gv
    End Function
#End Region

    Public Overrides Sub style_grid_many()
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
                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                        For Each ctrl_xtp In xtp.Controls
                                            If TypeOf ctrl_xtp Is DevExpress.XtraGrid.GridControl Then
                                                gc = CType(ctrl_xtp, DevExpress.XtraGrid.GridControl)
                                                gv = gc.MainView
                                                style_grid(gv, master_new.ClsVar.sGridStyle)
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Public Overrides Sub save_as()
        If MessageBox.Show("Save As This Form ...?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim form_alias As String = "", form_name As String = "", form_ori As String = ""
        Dim fixed As Boolean = False
        Dim fixed_alignment As String = ""
        form_alias = InputBox("Your New Form Name..", "Customize Form", "")

        If Trim(form_alias) = "" Then
            Exit Sub
        End If

        If type_form = True Then
            form_ori = Me.Name.ToLower
        Else
            Try
                Using objsearch As New master_new.WDABasepgsql("", "")
                    With objsearch
                        .Connection.Open()
                        .Command = .Connection.CreateCommand
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "select form_name_asal from tconfmycustom" + _
                                               " where form_name_baru = '" + Me.Name.ToLower + "'"
                        .InitializeCommand()
                        .DataReader = .Command.ExecuteReader
                        While .DataReader.Read
                            form_ori = .DataReader.Item("form_name_asal")
                        End While
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        Try
            Using objsearch As New master_new.WDABasepgsql("", "")
                With objsearch
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select count(form_alias) as jml from tconfmycustom" + _
                                           " where form_name_asal = '" + form_ori + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        form_name = form_ori + "_" + (.DataReader("jml") + 1).ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim i As Integer
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_sgp, DevExpress.XtraTab.XtraTabControl)
                                'active_page_name = xtc.SelectedTabPage.Name
                                For Each ctrl_xtc In xtc.Controls
                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                        'If xtp.Name = active_page_name Then
                                        For Each ctrl_xtp In xtp.Controls
                                            If TypeOf ctrl_xtp Is DevExpress.XtraGrid.GridControl Then
                                                gc = CType(ctrl_xtp, DevExpress.XtraGrid.GridControl)
                                                gv = gc.MainView
                                                Try
                                                    Using objtable As New master_new.WDABasepgsql("", "")
                                                        With objtable
                                                            .Connection.Open()
                                                            Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction
                                                            Try
                                                                .Command = .Connection.CreateCommand
                                                                .Command.Transaction = sqlTran
                                                                .Command.CommandType = CommandType.Text
                                                                For i = 0 To gv.Columns.Count - 1
                                                                    If gv.Columns(i).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left Then
                                                                        fixed = True
                                                                        fixed_alignment = "left"
                                                                    ElseIf gv.Columns(i).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None Then
                                                                        fixed = False
                                                                        fixed_alignment = "none"
                                                                    ElseIf gv.Columns(i).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right Then
                                                                        fixed = True
                                                                        fixed_alignment = "right"
                                                                    End If
                                                                    .Command.CommandText = "insert into tconf_columns values ('" + form_name.ToLower + "','" + _
                                                                                           gv.Name + "'," + ClsVar.sUserID.ToString + ",'" + _
                                                                                           gv.Columns(i).Name + "'," + gv.Columns(i).Visible.ToString + "," + _
                                                                                           gv.Columns(i).VisibleIndex.ToString + "," + _
                                                                                           gv.Columns(i).Visible.ToString + "," + _
                                                                                           gv.Columns(i).VisibleIndex.ToString + "," + _
                                                                                           fixed.ToString + ",'" + fixed_alignment + "')"
                                                                    .Command.ExecuteNonQuery()
                                                                    .Command.Parameters.Clear()
                                                                Next

                                                                sqlTran.Commit()
                                                                'fmm.load_mycustom()
                                                                'Exit Sub
                                                            Catch ex As nPgSqlException
                                                                sqlTran.Rollback()
                                                                MessageBox.Show(ex.Message)
                                                            End Try
                                                        End With
                                                    End Using
                                                Catch ex As nPgSqlException
                                                    MessageBox.Show(ex.Message)
                                                End Try
                                            End If
                                        Next
                                        'End If
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next

        Try
            Using objtable As New master_new.WDABasepgsql("", "")
                With objtable
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "insert into tconfmycustom values ('" + form_alias + "'," + _
                                               ClsVar.sUserID.ToString + ",'" + _
                                               form_ori + "','" + form_name + "')"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        fmm.load_mycustom()
                        'Exit Sub
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As nPgSqlException
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overrides Sub freeze()
        Dim i As Integer
        Dim col As Integer
        Dim status As Boolean = False

        gv = get_gv()

        For i = 0 To gv.Columns.Count - 1
            If gv.Columns(i).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left Then
                status = True
                Exit For
            End If
        Next

        save_setting_columns()
        gv = get_gv()

        If status = False Then
            col = gv.Columns.IndexOf(gv.FocusedColumn)
            For i = 0 To col
                gv.Columns(i).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            Next
            Try
                Using objtable As New master_new.WDABasepgsql("", "")
                    With objtable
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran
                            .Command.CommandType = CommandType.Text
                            For i = 0 To col
                                .Command.CommandText = "update tconf_columns set fixed = true, fixed_alignment = 'left' " + _
                                                       " where form_name ~~* '" + Me.Name.ToLower + "'" + _
                                                       " and gv_name ~~* '" + gv.Name + "'" + _
                                                       " and userid = " + ClsVar.sUserID.ToString + _
                                                       " and column_name ~~* '" + gv.Columns(i).Name + "'"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next

                            sqlTran.Commit()
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try
                    End With
                End Using
            Catch ex As nPgSqlException
                MessageBox.Show(ex.Message)
            End Try
        Else
            For i = 0 To gv.Columns.Count - 1
                gv.Columns(i).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None
            Next
            Try
                Using objtable As New master_new.WDABasepgsql("", "")
                    With objtable
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran
                            .Command.CommandType = CommandType.Text
                            For i = 0 To col
                                .Command.CommandText = "update tconf_columns set fixed = false, fixed_alignment = 'none' " + _
                                                       " where form_name ~~* '" + Me.Name.ToLower + "'" + _
                                                       " and gv_name ~~* '" + gv.Name + "'" + _
                                                       " and userid = " + ClsVar.sUserID.ToString
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next

                            sqlTran.Commit()
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try
                    End With
                End Using
            Catch ex As nPgSqlException
                MessageBox.Show(ex.Message)
            End Try
        End If

        load_Columns()
    End Sub

    Public Overrides Sub set_default()
        If MessageBox.Show("Set As Default For This Configuration Form..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If
        cek_folder()
        save_setting_columns()
        MessageBox.Show("Configuration Have Been Saved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Public Overrides Sub load_Columns()
        Dim nama_file, path, status_file As String
        path = "c:\syspro\layout\"

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
                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                        For Each ctrl_xtp In xtp.Controls
                                            If TypeOf ctrl_xtp Is DevExpress.XtraGrid.GridControl Then
                                                gc = CType(ctrl_xtp, DevExpress.XtraGrid.GridControl)
                                                gv = gc.MainView
                                                nama_file = Me.Name + "_" + gv.Name + "_" + master_new.ClsVar.sUserID.ToString + ".xml"
                                                status_file = DevExpress.Utils.FilesHelper.FindingFileName(path, nama_file, False)
                                                If status_file <> "" Then
                                                    gv.RestoreLayoutFromXml(path + nama_file)
                                                End If
                                            ElseIf TypeOf ctrl_xtp Is DevExpress.XtraPivotGrid.PivotGridControl Then
                                                xvg = CType(ctrl_xtp, DevExpress.XtraPivotGrid.PivotGridControl)
                                                nama_file = Me.Name + "_" + xvg.Name + "_" + master_new.ClsVar.sUserID.ToString + ".xml"
                                                status_file = DevExpress.Utils.FilesHelper.FindingFileName(path, nama_file, False)
                                                If status_file <> "" Then
                                                    xvg.RestoreLayoutFromXml(path + nama_file)
                                                End If
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Public Overrides Sub save_setting_columns()
        'Dim i As Integer
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
                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                        For Each ctrl_xtp In xtp.Controls
                                            If TypeOf ctrl_xtp Is DevExpress.XtraGrid.GridControl Then
                                                gc = CType(ctrl_xtp, DevExpress.XtraGrid.GridControl)
                                                gv = gc.MainView
                                                gv.SaveLayoutToXml("c:\syspro\layout\" + Me.Name + "_" + gv.Name + "_" + master_new.ClsVar.sUserID.ToString + ".xml")
                                            ElseIf TypeOf ctrl_xtp Is DevExpress.XtraPivotGrid.PivotGridControl Then
                                                xvg = CType(ctrl_xtp, DevExpress.XtraPivotGrid.PivotGridControl)
                                                xvg.SaveLayoutToXml("c:\syspro\layout\" + Me.Name + "_" + xvg.Name + "_" + master_new.ClsVar.sUserID.ToString + ".xml")
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Public Overrides Sub reset()
        If MessageBox.Show("Yakin Untuk Mereset Setingan Ini..", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim FileToDelete As String = ""

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
                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                        For Each ctrl_xtp In xtp.Controls
                                            If TypeOf ctrl_xtp Is DevExpress.XtraGrid.GridControl Then
                                                gc = CType(ctrl_xtp, DevExpress.XtraGrid.GridControl)
                                                gv = gc.MainView

                                                FileToDelete = "c:\syspro\layout\" + Me.Name + "_" + gv.Name + "_" + master_new.ClsVar.sUserID.ToString + ".xml"

                                                If System.IO.File.Exists(FileToDelete) = True Then
                                                    System.IO.File.Delete(FileToDelete)
                                                End If
                                            ElseIf TypeOf ctrl_xtp Is DevExpress.XtraPivotGrid.PivotGridControl Then
                                                xvg = CType(ctrl_xtp, DevExpress.XtraPivotGrid.PivotGridControl)

                                                FileToDelete = "c:\syspro\layout\" + Me.Name + "_" + xvg.Name + "_" + master_new.ClsVar.sUserID.ToString + ".xml"

                                                If System.IO.File.Exists(FileToDelete) = True Then
                                                    System.IO.File.Delete(FileToDelete)
                                                End If
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next

        MessageBox.Show("Data Have Been Reseted..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Public Overrides Sub preview()
        Dim obj As Object
        obj = get_gc()
        If TypeOf obj Is DevExpress.XtraGrid.GridControl Then
            obj.ShowPrintPreview()
        ElseIf TypeOf obj Is DevExpress.XtraPivotGrid.PivotGridControl Then
            obj.ShowPrintPreview()
        End If
    End Sub

    Public Overrides Sub email_data()
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")
        Dim obj As Object
        If fileName <> "" Then
            obj = get_gv()

            If TypeOf obj Is DevExpress.XtraGrid.Views.Grid.GridView Then
                ExportTo(obj, New ExportXlsProvider(fileName))
            ElseIf TypeOf obj Is DevExpress.XtraPivotGrid.PivotGridControl Then
                obj.ExportToXls(fileName)
            End If

            Dim frm As New FMail
            frm.sc_txtfile.Text = fileName
            frm.ShowDialog()
        End If
    End Sub

    Private Function get_gc() As Object
        get_gc = DBNull.Value

        'Dim active_page As Integer
        Dim active_page_name As String

        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_sgp, DevExpress.XtraTab.XtraTabControl)
                                'active_page = xtc.SelectedTabPageIndex
                                active_page_name = xtc.SelectedTabPage.Name
                                For Each ctrl_xtc In xtc.Controls
                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                        If xtp.Name = active_page_name Then
                                            For Each ctrl_xtp In xtp.Controls
                                                If TypeOf ctrl_xtp Is DevExpress.XtraGrid.GridControl Then
                                                    get_gc = CType(ctrl_xtp, DevExpress.XtraGrid.GridControl)
                                                ElseIf TypeOf ctrl_xtp Is DevExpress.XtraPivotGrid.PivotGridControl Then
                                                    get_gc = CType(ctrl_xtp, DevExpress.XtraPivotGrid.PivotGridControl)
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

    Public Overridable Sub approve_wf(ByVal par_colom As String, ByVal par_table As String, ByVal par_initial As String, ByVal par_type As String, _
                                   ByVal par_status_id As Integer, ByVal par_desc As String, ByVal par_user_approval As String, _
                                   ByVal par_ds As DataSet, ByVal par_gv As Object, ByVal par_title As String)
        If MessageBox.Show("Approve Data Selection..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If
        Dim sSQLs As New ArrayList
        Dim wf_seq, i, _max_seq As Integer
        Dim filename, user_wf, user_wf_email, format_email_bantu As String

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        .Command.CommandType = CommandType.Text

                        par_ds.Tables("os").AcceptChanges()
                        For i = 0 To par_ds.Tables("os").Rows.Count - 1
                            If par_ds.Tables("os").Rows(i).Item("status") = True Then

                                Try
                                    par_gv.Columns(par_initial + "_oid").FilterInfo = _
                                    New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[" & par_initial + "_oid]='" & ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString & "'")
                                Catch ex As Exception
                                End Try

                                .Command.CommandText = "update wf_mstr set wf_wfs_id = '" + par_status_id.ToString + "'," + _
                                           " wf_aprv_date = current_timestamp, " + _
                                           " wf_aprv_user = '" + master_new.ClsVar.sNama + "', " + _
                                           " wf_desc = '" + Trim(par_desc) + "', " + _
                                           " wf_iscurrent = 'N'," + _
                                           " wf_dt = current_timestamp, " + _
                                           " wf_date_to = null " + _
                                           " where wf_ref_code ~~* '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_code") + "'" + _
                                           " and wf_user_id in (" + par_user_approval + ")" + _
                                           " and wf_iscurrent ~~* 'Y'"
                                sSQLs.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                       " where wf_ref_code ~~* '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_code") + "'" + _
                                                       " and wf_seq = " + (par_ds.Tables("os").Rows(i).Item("wf_seq") + 1).ToString
                                sSQLs.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                _max_seq = mf.get_max_seq(par_ds.Tables("os").Rows(i).Item(par_initial + "_code"))

                                If _max_seq = par_ds.Tables("os").Rows(i).Item("wf_seq") Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'I'" + _
                                                   " where " + par_initial + "_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString + "'"
                                    sSQLs.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                End If

                                wf_seq = par_ds.Tables("os").Rows(i).Item("wf_seq") + 1
                                user_wf = mf.get_user_wf(par_ds.Tables("os").Rows(i).Item(par_initial + "_code"), wf_seq)

                                'user_wf_email = mf.get_email_address(user_wf)

                                'If user_wf_email <> "" Then
                                '    filename = "c:\syspro\" + par_ds.Tables("os").Rows(i).Item(par_initial + "_code") + ".xls"
                                '    ExportTo(par_gv, New ExportXlsProvider(filename))

                                '    format_email_bantu = mf.format_email(user_wf, par_ds.Tables("os").Rows(i).Item(par_initial + "_code"), par_type)
                                '    mf.sent_email(user_wf_email, "", mf.title_email(par_title, par_ds.Tables("os").Rows(i).Item(par_initial + "_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                                'End If

                            End If
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
                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        load_data_many(True)
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overridable Sub hold_wf(ByVal par_initial As String, ByVal par_status_id As Integer, ByVal par_desc As String, ByVal par_hold As Integer, _
                                      ByVal par_user_approval As String, ByVal par_ds As DataSet)
        If MessageBox.Show("Hold Data Selection..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If
        Dim sSQLs As New ArrayList
        Dim i As Integer

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        .Command.CommandType = CommandType.Text

                        par_ds.Tables("os").AcceptChanges()
                        For i = 0 To par_ds.Tables("os").Rows.Count - 1
                            If par_ds.Tables("os").Rows(i).Item("status") = True Then

                                .Command.CommandText = "update wf_mstr set wf_wfs_id = '" + par_status_id.ToString + "'," + _
                                           " wf_aprv_date = current_timestamp, " + _
                                           " wf_aprv_user = '" + master_new.ClsVar.sNama + "'," + _
                                           " wf_desc = '" + Trim(par_desc) + "'," + _
                                           " wf_iscurrent = 'Y'," + _
                                           " wf_dt = current_timestamp, " + _
                                           " wf_date_to = current_date + " + par_hold.ToString + _
                                           " where wf_ref_code ~~* '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_code") + "'" + _
                                           " and wf_user_id in (" + par_user_approval + ")" + _
                                           " and wf_iscurrent ~~* 'Y'"
                                sSQLs.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
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
                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Holded..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        load_data_many(True)
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overridable Sub cancel_wf(ByVal par_table As String, ByVal par_colom As String, ByVal par_initial As String, ByVal par_type As String, ByVal par_status_id As Integer, ByVal par_desc As String, _
                          ByVal par_user_approval As String, ByVal par_ds As DataSet, ByVal par_title As String)
        If MessageBox.Show("Cancel Data Selection..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If
        Dim sSQLs As New ArrayList

        Dim i As Integer

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        .Command.CommandType = CommandType.Text

                        par_ds.Tables("os").AcceptChanges()
                        For i = 0 To par_ds.Tables("os").Rows.Count - 1
                            If par_ds.Tables("os").Rows(i).Item("status") = True Then

                                .Command = .Connection.CreateCommand
                                .Command.Transaction = sqlTran
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_wfs_id = '" + par_status_id.ToString + "'," + _
                                                       " wf_aprv_date = current_timestamp, " + _
                                                       " wf_aprv_user = '" + master_new.ClsVar.sNama + "'," + _
                                                       " wf_desc = '" + Trim(par_desc) + "'," + _
                                                       " wf_iscurrent = 'N'," + _
                                                       " wf_dt = current_timestamp, " + _
                                                       " wf_date_to = null " + _
                                                       " where wf_ref_code ~~* '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_code") + "'" + _
                                                       " and wf_user_id in (" + par_user_approval + ")" + _
                                                       " and wf_iscurrent ~~* 'Y'"
                                sSQLs.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'X' " + _
                                                       " where " + par_initial + "_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString + "'"
                                sSQLs.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                If par_ds.Tables("os").Rows(i).Item("useremail") <> "" Then
                                    mf.sent_email(par_ds.Tables("os").Rows(i).Item("useremail"), "", mf.title_email(par_title, par_ds.Tables("os").Rows(i).Item(par_initial + "_code")), "Transaction Number : " + ds.Tables("os").Rows(i).Item(par_initial + "_code") + " Cancel Transaction By : " + master_new.ClsVar.sNama, master_new.ClsVar.sEmailSyspro, "")
                                End If
                            End If
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
                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Canceled..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        load_data_many(True)
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overridable Sub rollback_wf(ByVal par_table As String, ByVal par_colom As String, ByVal par_initial As String, ByVal par_type As String, ByVal par_status_id As Integer, ByVal par_desc As String, _
                          ByVal par_user_approval As String, ByVal par_ds As DataSet, ByVal par_title As String)
        If MessageBox.Show("Rollback Data Selection..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim i As Integer
        Dim sSQLs As New ArrayList
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        .Command.CommandType = CommandType.Text

                        par_ds.Tables("os").AcceptChanges()
                        For i = 0 To par_ds.Tables("os").Rows.Count - 1
                            If par_ds.Tables("os").Rows(i).Item("status") = True Then

                                .Command = .Connection.CreateCommand
                                .Command.Transaction = sqlTran
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_wfs_id = '" + par_status_id.ToString + "'," + _
                                                       " wf_aprv_date = current_timestamp, " + _
                                                       " wf_aprv_user = '" + master_new.ClsVar.sNama + "'," + _
                                                       " wf_desc = '" + Trim(par_desc) + "'," + _
                                                       " wf_iscurrent = 'N'," + _
                                                       " wf_dt = current_timestamp, " + _
                                                       " wf_date_to = null " + _
                                                       " where wf_ref_code ~~* '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_code") + "'" + _
                                                       " and wf_user_id in (" + par_user_approval + ")" + _
                                                       " and wf_iscurrent ~~* 'Y'"
                                sSQLs.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'D' " + _
                                                       " where " + par_initial + "_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString + "'"
                                sSQLs.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                If par_ds.Tables("os").Rows(i).Item("useremail") <> "" Then
                                    mf.sent_email(par_ds.Tables("os").Rows(i).Item("useremail"), "", mf.title_email(par_title, par_ds.Tables("os").Rows(i).Item(par_initial + "_code")), "Transaction Number : " + ds.Tables("os").Rows(i).Item(par_initial + "_code") + " Rollback Transaction By : " + master_new.ClsVar.sNama, master_new.ClsVar.sEmailSyspro, "")
                                End If
                            End If
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
                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Rollbacked..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        load_data_many(True)
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
