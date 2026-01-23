Public Class FReminderSystem

    Public fmm As master_new.MasterMDI

    Public Overridable Sub set_window(ByVal arg As master_new.MasterMDI)
        fmm = arg
    End Sub

    Public Overrides Sub sb_go_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New FReminderInfo
        frm.MdiParent = fmm
        frm.set_window(fmm)
        frm.Show()
        Me.Close()
    End Sub
End Class
