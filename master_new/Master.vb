Imports DevExpress.XtraGrid.Views.Base

Public Class Master

    Public Overridable ReadOnly Property ExportView() As BaseView
        Get
            Return Nothing
        End Get
    End Property

    Public Overridable Sub preview()
        Cursor = Cursors.WaitCursor
        If Not ExportView Is Nothing AndAlso Not Me.ExportView.GridControl Is Nothing Then
            'Me.ExportView.GridControl.ShowPreview()
            Me.ExportView.GridControl.ShowPrintPreview()
        End If
        Cursor = Cursors.Arrow
    End Sub

    Public Overridable Function export_data() As Boolean

    End Function

    Public Overridable Function insert_data() As Boolean

    End Function

    Public Overridable Function edit_data() As Boolean

    End Function

    Public Overridable Function after_edit() As Boolean

    End Function

    Public Overridable Function delete_data() As Boolean

    End Function

    Public Overridable Function cancel_data() As Boolean

    End Function

    Public Overridable Function save_data() As Boolean

    End Function

    Public Overridable Sub find()

    End Sub

    Public Overridable Sub refresh_data()

    End Sub

    Public Overridable Sub email_data()

    End Sub

    Public Overridable Sub set_default()

    End Sub

    Public Overridable Sub reset()

    End Sub

    Public Overridable Sub favorite()

    End Sub

    Public Overridable Sub save_as()

    End Sub

    Public Overridable Sub freeze()

    End Sub

    Public Overridable Sub approve_line()

    End Sub

    Public Overridable Sub cancel_line()

    End Sub

    Public Overridable Sub smart_approve()


      
    End Sub

    Public Overridable Sub reminder_mail()

    End Sub

    Public Overridable Sub style_grid(ByVal gv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal par_style As String)

    End Sub

    Public Overridable Sub style_grid_detail()

    End Sub

    Public Overridable Sub style_grid_many()

    End Sub

    Public Overridable Function get_gv() As Object
        get_gv = DBNull.Value

        Return get_gv
    End Function



    Private Sub Master_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            Dim my As MasterMDI = CType(Me.ParentForm, MasterMDI)
            my.configurasi_menu("nothing")
        Catch ex As Exception
        End Try
    End Sub

        Public Overridable Sub Master_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.Handled = True
            Return
        End If
    End Sub


    Public Overridable Sub change_lang()

    End Sub
End Class