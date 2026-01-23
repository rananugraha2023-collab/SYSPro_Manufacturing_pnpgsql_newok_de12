Public Class FUser

    Private Sub FUser_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        Try
            Dim ds_cb As New DataSet
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select groupid , groupnama from tconfgroup"
                    .InitializeCommand()
                    .FillDataSet(ds_cb, "HelpGroup")
                    sc_cbgroup.Properties.DataSource = ds_cb.Tables("HelpGroup")
                    sc_cbgroup.Properties.DisplayMember = ds_cb.Tables("HelpGroup").Columns("groupnama").ToString
                    sc_cbgroup.Properties.ValueMember = ds_cb.Tables("HelpGroup").Columns("groupid").ToString

                    .SQL = "select k.id_karyawan, nama_depan, coalesce(departement, divisi) as departement" + _
                           " from tkaryawan k " + _
                           " inner join tkaryawan_kerja kk on kk.id_karyawan = k.id_karyawan " + _
                           " left outer join tdepartement d on d.id_departement = kk.id_departement " + _
                           " left outer join tdivisi v on v.id_divisi = kk.id_divisi " + _
                           " order by nama_depan"
                    .InitializeCommand()
                    .FillDataSet(ds_cb, "karyawan")
                    sc_cbnama.Properties.DataSource = ds_cb.Tables("karyawan")
                    sc_cbnama.Properties.DisplayMember = ds_cb.Tables("karyawan").Columns("nama_depan").ToString
                    sc_cbnama.Properties.ValueMember = ds_cb.Tables("karyawan").Columns("id_karyawan").ToString
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "select userid, u.groupid,last_access, userkode, usernama, password, groupnama, last_access, u.id_karyawan, nama_depan " + _
                     " from  tconfuser u " + _
                     " inner join tconfgroup g on g.groupid = u.groupid " + _
                     " left outer join tkaryawan k on k.id_karyawan = u.id_karyawan " + _
                     " order by usernama"
        Return get_sequel
    End Function

    Public Overrides Sub format_grid()
        add_column(gv_master, "userkode", False)
        add_column(gv_master, "User", "usernama", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Password", "password", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Group", "groupnama", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Nama Karyawan", "nama_depan", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Last Access", "last_access", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy")
    End Sub

    Public Overrides Sub param_insert(ByVal obj As Object)
        Dim userid As Integer
        Try
            Using objbantu As New master_new.WDABasepgsql("", "")
                With objbantu
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(max(userid),0) +1 as max_id from tconfuser"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read()
                        userid = .DataReader.Item("max_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        obj.AddParameter("@userid", userid)
        obj.AddParameter("@userkode", Trim(sc_txtkode_1.Text))
        obj.AddParameter("@usernama", Trim(sc_txtuser.Text))
        obj.AddParameter("@password", Trim(sc_txtpassword.Text))
        obj.AddParameter("@groupid", sc_cbgroup.GetColumnValue("groupid"))
        obj.AddParameter("@id_karyawan", sc_cbnama.GetColumnValue("id_karyawan"))
    End Sub

    Public Overrides Sub param_edit(ByVal obj As Object)
        obj.AddParameter("@userid", ds.Tables(0).Rows(row).Item("userid"))
        obj.AddParameter("@userkode", Trim(sc_txtkode_1.Text))
        obj.AddParameter("@usernama", Trim(sc_txtuser.Text))
        obj.AddParameter("@password", Trim(sc_txtpassword.Text))
        obj.AddParameter("@groupid", sc_cbgroup.GetColumnValue("groupid"))
        obj.AddParameter("@id_karyawan", sc_cbnama.GetColumnValue("id_karyawan"))
    End Sub

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                sc_txtkode_1.Text = .Item("userkode")
                sc_txtuser.Text = .Item("usernama")
                sc_txtpassword.Text = .Item("password")
                sc_cbgroup.EditValue = .Item("groupid")

                If IsDBNull(.Item("id_karyawan")) = False Then
                    sc_cbnama.EditValue = .Item("id_karyawan")
                End If
            End With

            edit_data = True
        End If
    End Function
End Class
