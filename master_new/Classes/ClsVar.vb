Public Class ClsVar
    Public Shared sUserID As Integer
    Public Shared sPassword As String
    Public Shared sGroupID As Integer
    Public Shared sNama As String
    Public Shared sNamaKaryawan As String
    Public Shared sFormSkin As String
    Public Shared SMainMenuStyle As String
    Public Shared sGridStyle As String
    Public Shared sKaryawanID As Integer
    Public Shared sPass As Boolean
    Public Shared sServerName As String
    Public Shared sNik As String
    Public Shared sEmailSyspro As String = ModFunction.konfigurasi(ModFunction.GetFileContents(ModFunction.appbase() & "\filekonfigurasi\pgserversync.txt"), "email") '"syspromail@sygmacorp.com"
    Public Shared sTimeInterval As Integer
    Public Shared _oid As Guid
    Public Shared sBody As String = "The attach file contains the description of Permohonan Dana." + vbCrLf + "Please do the approval."
    Public Shared _filename As String
    Public Shared sdom_id As String
    Public Shared ibase_cur_id As Integer
    Public Shared sServerCode As String
    Public Shared CExit As Boolean = True

    Public Shared email_server_name As String = ModFunction.konfigurasi(ModFunction.GetFileContents(ModFunction.appbase() & "\filekonfigurasi\conf_system.txt"), "email_server_name")
    Public Shared email_user_name As String = ModFunction.konfigurasi(ModFunction.GetFileContents(ModFunction.appbase() & "\filekonfigurasi\conf_system.txt"), "email_user_name")
    Public Shared email_port As String = ModFunction.konfigurasi(ModFunction.GetFileContents(ModFunction.appbase() & "\filekonfigurasi\conf_system.txt"), "email_port")
    Public Shared email_ssl As String = ModFunction.konfigurasi(ModFunction.GetFileContents(ModFunction.appbase() & "\filekonfigurasi\conf_system.txt"), "email_ssl")
    Public Shared email_password As String = "sygma"
    Public Shared email_cc As String = ModFunction.konfigurasi(ModFunction.GetFileContents(ModFunction.appbase() & "\filekonfigurasi\conf_system.txt"), "email_cc")
    Public Shared par_lang As String = master_new.ModFunction.GetFileContents(master_new.ModFunction.appbase() & _
                            "\filekonfigurasi\lang.txt")

    Public Shared _status_sync As Boolean = False

End Class

