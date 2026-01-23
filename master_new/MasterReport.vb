Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Windows.Forms

Public Class MasterReport
    Dim pdvarg1 As CrystalDecisions.Shared.ParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
    Dim pdvarg2 As CrystalDecisions.Shared.ParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
    Dim pdvarg3 As CrystalDecisions.Shared.ParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
    Dim pdvarg4 As CrystalDecisions.Shared.ParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
    Dim pdvarg5 As CrystalDecisions.Shared.ParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
    Dim pdvarg6 As CrystalDecisions.Shared.ParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
    Dim pdvarg7 As CrystalDecisions.Shared.ParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
    Dim pdvarg8 As CrystalDecisions.Shared.ParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
    Dim pdvarg9 As CrystalDecisions.Shared.ParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
    Dim pdvarg10 As CrystalDecisions.Shared.ParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
    Dim pdvarg11 As CrystalDecisions.Shared.ParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
    Dim pdvarg12 As CrystalDecisions.Shared.ParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
    Dim pdvarg13 As CrystalDecisions.Shared.ParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue

    Dim pvCollection As CrystalDecisions.Shared.ParameterValues = New CrystalDecisions.Shared.ParameterValues
    Dim tliCurrent As CrystalDecisions.Shared.TableLogOnInfo

    Public myParameterFields As New ParameterFields()
    Public myParameterField As New ParameterField()
    Public myDiscreteValue As New ParameterDiscreteValue()

    Public rpt As ReportDocument = New CrystalDecisions.CrystalReports.Engine.ReportDocument

    'Public fmm As MasterVB.MasterWork

    'Public Overridable Sub set_window(ByVal arg As MasterVB.Master)
    '    fmm = arg
    'End Sub

    Private Sub MasterReport_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Dim my As MasterMDI = CType(Me.ParentForm, MasterMDI)
        my.configurasi_menu("nothing")
    End Sub

    Public Overridable Sub preview_report(ByVal sPath As String)
        Try
            rpt.Load(sPath)

            For Each tbCurent As CrystalDecisions.CrystalReports.Engine.Table In rpt.Database.Tables
                tliCurrent = tbCurent.LogOnInfo
                tliCurrent.ConnectionInfo.ServerName = "hariff10" 'SqlDB.sServerName
                tliCurrent.ConnectionInfo.UserID = "user1" 'SqlDB.sUserID
                tliCurrent.ConnectionInfo.Password = "u" 'SqlDB.sPassword
                tliCurrent.ConnectionInfo.DatabaseName = "hariff" 'SqlDB.sDatabaseName
                tbCurent.ApplyLogOnInfo(tliCurrent)
            Next

            getparam()

            CRV.DisplayGroupTree = False
            'rpt.SetDataSource(fmm.ds)
            CRV.ReportSource = rpt
            CRV.Zoom(1)
        Catch ex As LoadSaveReportException
            MessageBox.Show("Incorrect path for loading report.", "Load Report Error")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "General Error")
        End Try
    End Sub

    Public Overridable Sub preview_report_ds(ByVal sPath As String, ByRef datasource As DataTable)
        Try
            rpt.Load(sPath)
            getparam()
            CRV.DisplayGroupTree = False
            rpt.SetDataSource(datasource)
            'rpt.Subreports("").SetDataSource()
            CRV.ReportSource = rpt
            CRV.Zoom(1)
        Catch ex As LoadSaveReportException
            MessageBox.Show("Incorrect path for loading report.", "Load Report Error")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "General Error")
        End Try
    End Sub

    Public Overridable Sub preview_report_sql(ByVal sPath As String)
        Try
            rpt.Load(sPath)
            'Dim string_s As String
            'string_s = System.Net.Dns.GetHostName
            'string_s = string_s + "\sqlexpress"

            For Each tbCurent As CrystalDecisions.CrystalReports.Engine.Table In rpt.Database.Tables
                tliCurrent = tbCurent.LogOnInfo
                tliCurrent.ConnectionInfo.ServerName = "10.1.1.254"
                'tliCurrent.ConnectionInfo.ServerName = "localhost"
                tliCurrent.ConnectionInfo.UserID = "syssetiadi"
                tliCurrent.ConnectionInfo.Password = "syspro"
                tliCurrent.ConnectionInfo.DatabaseName = "mfgpro"
                tbCurent.ApplyLogOnInfo(tliCurrent)
            Next

            getparam()

            CRV.DisplayGroupTree = False
            CRV.ReportSource = rpt
            CRV.Zoom(1)
        Catch ex As LoadSaveReportException
            MessageBox.Show("Incorrect path for loading report.", "Load Report Error")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "General Error")
        End Try
    End Sub

    Private Sub getparam()

        Dim i As Integer = 0 ', j As Integer
        Dim apasih As String

        For Each cntrl In Me.Controls
            If TypeOf cntrl Is Panel Then
                pnl = CType(cntrl, Panel)
                apasih = pnl.Name
                For Each cntrl_spp In pnl.Controls
                    If cntrl_spp.Name.Length > 4 Then
                        If cntrl_spp.Name.Substring(0, 4) = "pr_l" Then
                            i = i + 1
                        End If
                    End If
                Next
            End If
        Next

        If i = 1 Then
            addparam(pdvarg1, False)
        ElseIf i = 2 Then
            addparam(pdvarg1, True)
            addparam(pdvarg2, False)
        ElseIf i = 3 Then
            addparam(pdvarg1, True)
            addparam(pdvarg2, True)
            addparam(pdvarg3, False)
        ElseIf i = 4 Then
            addparam(pdvarg1, True)
            addparam(pdvarg2, True)
            addparam(pdvarg3, True)
            addparam(pdvarg4, False)
        ElseIf i = 5 Then
            addparam(pdvarg1, True)
            addparam(pdvarg2, True)
            addparam(pdvarg3, True)
            addparam(pdvarg4, True)
            addparam(pdvarg5, False)
        ElseIf i = 6 Then
            addparam(pdvarg1, True)
            addparam(pdvarg2, True)
            addparam(pdvarg3, True)
            addparam(pdvarg4, True)
            addparam(pdvarg5, True)
            addparam(pdvarg6, False)
        ElseIf i = 7 Then
            addparam(pdvarg1, True)
            addparam(pdvarg2, True)
            addparam(pdvarg3, True)
            addparam(pdvarg4, True)
            addparam(pdvarg5, True)
            addparam(pdvarg6, True)
            addparam(pdvarg7, False)
        ElseIf i = 8 Then
            addparam(pdvarg1, True)
            addparam(pdvarg2, True)
            addparam(pdvarg3, True)
            addparam(pdvarg4, True)
            addparam(pdvarg5, True)
            addparam(pdvarg6, True)
            addparam(pdvarg7, True)
            addparam(pdvarg8, False)
        ElseIf i = 9 Then
            addparam(pdvarg1, True)
            addparam(pdvarg2, True)
            addparam(pdvarg3, True)
            addparam(pdvarg4, True)
            addparam(pdvarg5, True)
            addparam(pdvarg6, True)
            addparam(pdvarg7, True)
            addparam(pdvarg8, True)
            addparam(pdvarg9, True)
        ElseIf i = 10 Then
            addparam(pdvarg1, True)
            addparam(pdvarg2, True)
            addparam(pdvarg3, True)
            addparam(pdvarg4, True)
            addparam(pdvarg5, True)
            addparam(pdvarg6, True)
            addparam(pdvarg7, True)
            addparam(pdvarg8, True)
            addparam(pdvarg9, True)
            addparam(pdvarg10, False)
        ElseIf i = 11 Then
            addparam(pdvarg1, True)
            addparam(pdvarg2, True)
            addparam(pdvarg3, True)
            addparam(pdvarg4, True)
            addparam(pdvarg5, True)
            addparam(pdvarg6, True)
            addparam(pdvarg7, True)
            addparam(pdvarg8, True)
            addparam(pdvarg9, True)
            addparam(pdvarg10, False)
            addparam(pdvarg11, False)
        ElseIf i = 12 Then
            addparam(pdvarg1, True)
            addparam(pdvarg2, True)
            addparam(pdvarg3, True)
            addparam(pdvarg4, True)
            addparam(pdvarg5, True)
            addparam(pdvarg6, True)
            addparam(pdvarg7, True)
            addparam(pdvarg8, True)
            addparam(pdvarg9, True)
            addparam(pdvarg10, False)
            addparam(pdvarg11, False)
            addparam(pdvarg12, False)
        ElseIf i = 13 Then
            addparam(pdvarg1, True)
            addparam(pdvarg2, True)
            addparam(pdvarg3, True)
            addparam(pdvarg4, True)
            addparam(pdvarg5, True)
            addparam(pdvarg6, True)
            addparam(pdvarg7, True)
            addparam(pdvarg8, True)
            addparam(pdvarg9, True)
            addparam(pdvarg10, False)
            addparam(pdvarg11, False)
            addparam(pdvarg12, False)
            addparam(pdvarg13, False)
        End If
        CRV.ParameterFieldInfo = myParameterFields
    End Sub

    Public Overridable Sub addparam(ByVal arg As CrystalDecisions.Shared.ParameterDiscreteValue, ByVal cleer As Boolean)
        Dim pr, nilai As String
        Dim i As Integer
        nilai = 0
        For Each cntrl In Me.Controls
            If TypeOf cntrl Is Panel Then
                pnl = CType(cntrl, Panel)
                For Each cntrl_spp In pnl.Controls
                    If cntrl_spp.Name.Length > 4 Then
                        If cntrl_spp.Name.Substring(0, 4) = "pr_l" Then
                            pr = cntrl_spp.Name.Substring(6, Len(cntrl_spp.Name) - 6)

                            For i = 0 To cntrl_spp.Text.Length - 1
                                If cntrl_spp.Text.Chars(i) = "%" Then
                                    nilai = nilai + "*"
                                Else
                                    nilai = nilai + cntrl_spp.Text.Chars(i)
                                End If

                            Next

                            cntrl_spp.Text = nilai

                            myParameterField = New ParameterField
                            myDiscreteValue = New ParameterDiscreteValue()

                            myParameterField.ParameterFieldName = pr
                            myDiscreteValue.Value = cntrl_spp.Text
                            myParameterField.CurrentValues.Add(myDiscreteValue)

                            myParameterFields.Add(myParameterField)

                            cntrl_spp.Name = "geted" + cntrl_spp.Name
                            Exit Sub
                        End If
                    End If
                Next
            End If
        Next
    End Sub
End Class
