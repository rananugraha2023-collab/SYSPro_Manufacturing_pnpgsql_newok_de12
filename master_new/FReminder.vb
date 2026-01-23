Imports System.Media
Imports master_new.ModFunction

Public Class FReminder

    Dim awal As Integer = 0
    Dim akhir As Integer = 100
    Private Sub FReminder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        timer_awal.Enabled = True
        timer_ring.Enabled = True
    End Sub

    Private Sub timer_awal_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer_awal.Tick
        awal += 1
        Me.Opacity = awal / 100
        If awal = 100 Then
            timer_awal.Enabled = False
            timer_akhir.Enabled = True
            akhir = 100
        End If
    End Sub

    Private Sub timer_akhir_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer_akhir.Tick
        akhir -= 1
        Me.Opacity = akhir / 100
        If akhir = 0 Then
            timer_akhir.Enabled = False
            timer_awal.Enabled = True
            awal = 0
        End If
    End Sub

    Private Sub timer_ring_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer_ring.Tick
        Dim objPlayer As New SoundPlayer
        objPlayer.SoundLocation = appbase() + "\zpendukung\ringback.wav"
        objPlayer.Play()
    End Sub

    Public Overridable Sub sb_go_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_go.Click

    End Sub

   
End Class