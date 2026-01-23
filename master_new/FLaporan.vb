Imports CrystalDecisions.CrystalReports.Engine

Public Class FLaporan
    Public Overridable Sub preview_report_ds_mutiple(ByVal sPath As String, ByRef datasource As DataTable, ByRef pendidikan As DataTable)
        Try
            rpt.Load(sPath)
            'getparam()
            CRV.DisplayGroupTree = False
            rpt.SetDataSource(datasource)
            rpt.Subreports(0).SetDataSource(pendidikan)
            CRV.ReportSource = rpt
            CRV.Zoom(1)
        Catch ex As LoadSaveReportException
            MessageBox.Show("Incorrect path for loading report.", "Load Report Error")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "General Error")
        End Try
    End Sub
End Class
